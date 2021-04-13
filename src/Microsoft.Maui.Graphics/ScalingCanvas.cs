using System;
using System.Collections.Generic;
using Microsoft.Maui.Graphics.Text;

namespace Microsoft.Maui.Graphics
{
    public class ScalingCanvas : ICanvas, IBlurrableCanvas
    {
        private readonly ICanvas _canvas;
        private readonly IBlurrableCanvas _blurrableCanvas;
        private readonly Stack<double> _scaleXStack = new Stack<double>();
        private readonly Stack<double> _scaleYStack = new Stack<double>();
        private double _scaleX = 1f;
        private double _scaleY = 1f;

        public ScalingCanvas(ICanvas wrapped)
        {
            _canvas = wrapped;
            _blurrableCanvas = _canvas as IBlurrableCanvas;
        }

        public double RetinaScale
        {
            get => _canvas.RetinaScale;
            set => _canvas.RetinaScale = value;
        }

        public double DisplayScale
        {
            get => _canvas.DisplayScale;
            set => _canvas.DisplayScale = value;
        }

        public object Wrapped => _canvas;

        public ICanvas ParentCanvas => _canvas;

        public double StrokeSize
        {
            set => _canvas.StrokeSize = value;
        }

        public double MiterLimit
        {
            set => _canvas.MiterLimit = value;
        }

        public Color StrokeColor
        {
            set => _canvas.StrokeColor = value;
        }

        public LineCap StrokeLineCap
        {
            set => _canvas.StrokeLineCap = value;
        }

        public double Alpha
        {
            set => _canvas.Alpha = value;
        }

        public LineJoin StrokeLineJoin
        {
            set => _canvas.StrokeLineJoin = value;
        }

        public double[] StrokeDashPattern
        {
            set => _canvas.StrokeDashPattern = value;
        }

        public Color FillColor
        {
            set => _canvas.FillColor = value;
        }

        public Color FontColor
        {
            set => _canvas.FontColor = value;
        }

        public string FontName
        {
            set => _canvas.FontName = value;
        }

        public double FontSize
        {
            set => _canvas.FontSize = value;
        }

        public BlendMode BlendMode
        {
            set => _canvas.BlendMode = value;
        }

        public bool Antialias
        {
            set => _canvas.Antialias = value;
        }

        public void SubtractFromClip(double x1, double y1, double x2, double y2)
        {
            _canvas.SubtractFromClip(x1 * _scaleX, y1 * _scaleY, x2 * _scaleX, y2 * _scaleY);
        }

        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            _canvas.DrawLine(x1 * _scaleX, y1 * _scaleY, x2 * _scaleX, y2 * _scaleY);
        }

        public void DrawArc(double x, double y, double width, double height, double startAngle, double endAngle, bool clockwise, bool closed)
        {
            _canvas.DrawArc(x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY, startAngle, endAngle, clockwise, closed);
        }

        public void FillArc(double x, double y, double width, double height, double startAngle, double endAngle, bool clockwise)
        {
            _canvas.FillArc(x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY, startAngle, endAngle, clockwise);
        }

        public void DrawEllipse(double x, double y, double width, double height)
        {
            _canvas.DrawEllipse(x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY);
        }

        public void DrawImage(IImage image, double x, double y, double width, double height)
        {
            _canvas.DrawImage(image, x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY);
        }

        public void DrawRectangle(double x, double y, double width, double height)
        {
            _canvas.DrawRectangle(x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY);
        }

        public void DrawRoundedRectangle(double x, double y, double width, double height, double cornerRadius)
        {
            _canvas.DrawRoundedRectangle(x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY, cornerRadius * _scaleX);
        }

        public void DrawString(string value, double x, double y, HorizontalAlignment horizontalAlignment)
        {
            _canvas.DrawString(value, x * _scaleX, y * _scaleY, horizontalAlignment);
        }

        public void DrawString(string value, double x, double y, double width, double height, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment,
            TextFlow textFlow = TextFlow.ClipBounds, double lineSpacingAdjustment = 0)
        {
            _canvas.DrawString(value, x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY, horizontalAlignment, verticalAlignment, textFlow);
        }

        public void DrawText(IAttributedText value, double x, double y, double width, double height)
        {
            _canvas.DrawText(value, x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY);
        }

        public void FillEllipse(double x, double y, double width, double height)
        {
            _canvas.FillEllipse(x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY);
        }

        public void FillRectangle(double x, double y, double width, double height)
        {
            _canvas.FillRectangle(x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY);
        }

        public void FillRoundedRectangle(double x, double y, double width, double height, double cornerRadius)
        {
            _canvas.FillRoundedRectangle(x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY, cornerRadius * _scaleX);
        }

        public void DrawPath(Path path)
        {
            var scaledPath = path.AsScaledPath(_scaleX);
            _canvas.DrawPath(scaledPath);
        }

        public void FillPath(Path path, WindingMode windingMode)
        {
            var scaledPath = path.AsScaledPath(_scaleX);
            _canvas.FillPath(scaledPath, windingMode);
        }

        public void ClipPath(Path path, WindingMode windingMode = WindingMode.NonZero)
        {
            var scaledPath = path.AsScaledPath(_scaleX);
            _canvas.ClipPath(scaledPath, windingMode);
        }

        public void ClipRectangle(double x, double y, double width, double height)
        {
            _canvas.ClipRectangle(x * _scaleX, y * _scaleY, width * _scaleX, height * _scaleY);
        }

        public void Rotate(double degrees, double x, double y)
        {
            _canvas.Rotate(degrees, x * _scaleX, y * _scaleY);
        }

        public void SetFillPaint(Paint paint, double x1, double y1, double x2, double y2)
        {
            _canvas.SetFillPaint(paint, x1 * _scaleX, y1 * _scaleY, x2 * _scaleX, y2 * _scaleY);
        }

        public void Rotate(double degrees)
        {
            _canvas.Rotate(degrees);
        }

        public void Scale(double sx, double sy)
        {
            _scaleX *= Math.Abs(sx);
            _scaleY *= Math.Abs(sy);
            _canvas.Scale(sx, sy);
        }

        public void Translate(double tx, double ty)
        {
            _canvas.Translate(tx, ty);
        }

        public void ConcatenateTransform(AffineTransform transform)
        {
            _scaleX *= transform.ScaleX;
            _scaleY *= transform.ScaleY;
            _canvas.ConcatenateTransform(transform);
        }

        public void SaveState()
        {
            _canvas.SaveState();
            _scaleXStack.Push(_scaleX);
            _scaleYStack.Push(_scaleY);
        }

        public void ResetState()
        {
            _canvas.ResetState();
            _scaleXStack.Clear();
            _scaleYStack.Clear();
            _scaleX = 1;
            _scaleY = 1;
        }

        public bool RestoreState()
        {
            var restored = _canvas.RestoreState();
            if (_scaleXStack.Count > 0)
            {
                _scaleX = _scaleXStack.Pop();
                _scaleY = _scaleYStack.Pop();
            }
            else
            {
                _scaleX = 1;
                _scaleY = 1;
            }

            return restored;
        }

        public double GetScale()
        {
            return _scaleX;
        }

        public void SetShadow(Size offset, double blur, Color color)
        {
            _canvas.SetShadow(offset, blur, color);
        }

        public void SetToSystemFont()
        {
            _canvas.SetToSystemFont();
        }

        public void SetToBoldSystemFont()
        {
            _canvas.SetToBoldSystemFont();
        }

        public void SetBlur(double blurRadius)
        {
            _blurrableCanvas?.SetBlur(blurRadius);
        }
    }
}