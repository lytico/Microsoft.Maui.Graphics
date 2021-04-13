using System;
using System.Collections.Generic;
using Microsoft.Maui.Graphics.Text;

namespace Microsoft.Maui.Graphics
{
    public abstract class AbstractCanvas<TState> : ICanvas, IDisposable where TState : CanvasState
    {
        private readonly Stack<TState> _stateStack = new Stack<TState>();
        private readonly Func<object, TState> _createNew;
        private readonly Func<TState, TState> _createCopy;

        private TState _currentState;
        private bool _limitStrokeScaling;
        private double _strokeLimit = 1;
        private bool _strokeDashPatternDirty;

        protected abstract double NativeStrokeSize { set; }
        protected abstract void NativeSetStrokeDashPattern(double[] pattern, double strokeSize);
        protected abstract void NativeDrawLine(double x1, double y1, double x2, double y2);
        protected abstract void NativeDrawArc(double x, double y, double width, double height, double startAngle, double endAngle, bool clockwise, bool closed);
        protected abstract void NativeDrawRectangle(double x, double y, double width, double height);
        protected abstract void NativeDrawRoundedRectangle(double x, double y, double width, double height, double cornerRadius);
        protected abstract void NativeDrawEllipse(double x, double y, double width, double height);
        protected abstract void NativeDrawPath(Path path);
        protected abstract void NativeRotate(double degrees, double radians, double x, double y);
        protected abstract void NativeRotate(double degrees, double radians);
        protected abstract void NativeScale(double fx, double fy);
        protected abstract void NativeTranslate(double tx, double ty);
        protected abstract void NativeConcatenateTransform(AffineTransform transform);

        protected AbstractCanvas(Func<object, TState> createNew, Func<TState, TState> createCopy)
        {
            _createCopy = createCopy;
            _createNew = createNew;
            _currentState = createNew(this);
        }

        protected TState CurrentState => _currentState;

        public virtual void Dispose()
        {
            if (_currentState != null)
            {
                _currentState.Dispose();
                _currentState = null;
            }
        }

        public bool LimitStrokeScaling
        {
            set => _limitStrokeScaling = value;
        }

        protected bool LimitStrokeScalingEnabled => _limitStrokeScaling;

        public double StrokeLimit
        {
            set => _strokeLimit = value;
        }

        public abstract Color FillColor { set; }
        public abstract Color FontColor { set; }
        public abstract string FontName { set; }
        public abstract double FontSize { set; }
        public abstract double Alpha { set; }
        public abstract bool Antialias { set; }
        public abstract BlendMode BlendMode { set; }

        protected double AssignedStrokeLimit => _strokeLimit;

        public virtual double DisplayScale { get; set; }

        public double RetinaScale { get; set; }

        public double StrokeSize
        {
            set
            {
                var size = value;

                if (_limitStrokeScaling)
                {
                    var scale = _currentState.Scale;
                    var scaledStrokeSize = scale * value;
                    if (scaledStrokeSize < _strokeLimit)
                    {
                        size = _strokeLimit / scale;
                    }
                }

                _currentState.StrokeSize = size;
                NativeStrokeSize = size;
            }
        }

        public abstract double MiterLimit { set; }
        public abstract Color StrokeColor { set; }
        public abstract LineCap StrokeLineCap { set; }
        public abstract LineJoin StrokeLineJoin { set; }

        public double[] StrokeDashPattern
        {
            set
            {
                if (!ReferenceEquals(value, _currentState.StrokeDashPattern))
                {
                    _currentState.StrokeDashPattern = value;
                    _strokeDashPatternDirty = true;
                }
            }
        }

        private void EnsureStrokePatternSet()
        {
            if (_strokeDashPatternDirty)
            {
                NativeSetStrokeDashPattern(_currentState.StrokeDashPattern, _currentState.StrokeSize);
                _strokeDashPatternDirty = false;
            }
        }

        public abstract void ClipRectangle(double x, double y, double width, double height);

        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            EnsureStrokePatternSet();
            NativeDrawLine(x1, y1, x2, y2);
        }

        public void DrawArc(double x, double y, double width, double height, double startAngle, double endAngle, bool clockwise, bool closed)
        {
            EnsureStrokePatternSet();
            NativeDrawArc(x, y, width, height, startAngle, endAngle, clockwise, closed);
        }

        public abstract void FillArc(double x, double y, double width, double height, double startAngle, double endAngle, bool clockwise);

        public void DrawRectangle(double x, double y, double width, double height)
        {
            EnsureStrokePatternSet();
            NativeDrawRectangle(x, y, width, height);
        }

        public abstract void FillRectangle(double x, double y, double width, double height);

        public void DrawRoundedRectangle(double x, double y, double width, double height, double cornerRadius)
        {
            var halfHeight = Math.Abs(height / 2);
            if (cornerRadius > halfHeight)
                cornerRadius = halfHeight;

            var halfWidth = Math.Abs(width / 2);
            if (cornerRadius > halfWidth)
                cornerRadius = halfWidth;

            EnsureStrokePatternSet();
            NativeDrawRoundedRectangle(x, y, width, height, cornerRadius);
        }

        public abstract void FillRoundedRectangle(double x, double y, double width, double height, double cornerRadius);

        public void DrawEllipse(double x, double y, double width, double height)
        {
            EnsureStrokePatternSet();
            NativeDrawEllipse(x, y, width, height);
        }

        public abstract void FillEllipse(double x, double y, double width, double height);
        public abstract void DrawString(string value, double x, double y, HorizontalAlignment horizontalAlignment);

        public abstract void DrawString(
            string value,
            double x,
            double y,
            double width,
            double height,
            HorizontalAlignment horizontalAlignment,
            VerticalAlignment verticalAlignment,
            TextFlow textFlow = TextFlow.ClipBounds,
            double lineSpacingAdjustment = 0);

        public abstract void DrawText(IAttributedText value, double x, double y, double width, double height);

        public void DrawPath(Path path)
        {
            EnsureStrokePatternSet();
            NativeDrawPath(path);
        }

        public abstract void FillPath(Path path, WindingMode windingMode);
        public abstract void SubtractFromClip(double x, double y, double width, double height);
        public abstract void ClipPath(Path path, WindingMode windingMode = WindingMode.NonZero);

        public virtual void ResetState()
        {
            while (_stateStack.Count > 0)
            {
                if (_currentState != null)
                {
                    _currentState.Dispose();
                    _currentState = null;
                }

                _currentState = _stateStack.Pop();
                StateRestored(_currentState);
            }

            if (_currentState != null)
            {
                _currentState.Dispose();
                _currentState = null;
            }

            _currentState = _createNew(this);
        }

        public abstract void SetShadow(Size offset, double blur, Color color);
        public abstract void SetFillPaint(Paint paint, double x1, double y1, double x2, double y2);
        public abstract void SetToSystemFont();
        public abstract void SetToBoldSystemFont();
        public abstract void DrawImage(IImage image, double x, double y, double width, double height);

        public virtual bool RestoreState()
        {
            _strokeDashPatternDirty = true;

            if (_currentState != null)
            {
                _currentState.Dispose();
                _currentState = null;
            }

            if (_stateStack.Count > 0)
            {
                _currentState = _stateStack.Pop();
                StateRestored(_currentState);
                return true;
            }

            _currentState = _createNew(this);
            return false;
        }

        protected virtual void StateRestored(TState state)
        {
            // Do nothing
        }

        public virtual void SaveState()
        {
            var previousState = _currentState;
            _stateStack.Push(previousState);
            _currentState = _createCopy(previousState);
            _strokeDashPatternDirty = true;
        }

        public void Rotate(double degrees, double x, double y)
        {
            var radians = Geometry.DegreesToRadians(degrees);

            var transform = _currentState.Transform;
            transform.Translate(x, y);
            transform.Rotate(radians);
            transform.Translate(-x, -y);

            NativeRotate(degrees, radians, x, y);
        }

        public void Rotate(double degrees)
        {
            var radians = Geometry.DegreesToRadians(degrees);
            _currentState.Transform.Rotate(radians);

            NativeRotate(degrees, radians);
        }

        public void Scale(double fx, double fy)
        {
            _currentState.Scale *= fx;
            _currentState.Transform.Scale(fx, fy);

            NativeScale(fx, fy);
        }

        public void Translate(double tx, double ty)
        {
            _currentState.Transform.Translate(tx, ty);
            NativeTranslate(tx, ty);
        }

        public void ConcatenateTransform(AffineTransform transform)
        {
            _currentState.Scale *= transform.ScaleX;
            _currentState.Transform.Concatenate(transform);
            NativeConcatenateTransform(transform);
        }
    }
}