using System;
using System.Collections.Generic;
using Microsoft.Maui.Graphics.Text;

namespace Microsoft.Maui.Graphics
{
    public class PictureCanvas : ICanvas, IDisposable
    {
        private readonly double _x;
        private readonly double _y;
        private readonly double _width;
        private readonly double _height;
        private readonly List<DrawingCommand> _commands;

        public PictureCanvas(double x, double y, double width, double height)
        {
            _x = x;
            _y = y;
            _height = height;
            _width = width;

            _commands = new List<DrawingCommand>();
        }

        public IPicture Picture => new StandardPicture(_x, _y, _width, _height, _commands.ToArray());

        public void Dispose()
        {
            _commands.Clear();
        }

        public double DisplayScale { get; set; } = 1;

        public double RetinaScale { get; set; } = 1;

        public double StrokeSize
        {
            set
            {
                _commands.Add(
                    canvas =>
                        canvas.StrokeSize = value
                );
            }
        }

        public double MiterLimit
        {
            set { _commands.Add(canvas => canvas.MiterLimit = value); }
        }

        public Color StrokeColor
        {
            set { _commands.Add(canvas => canvas.StrokeColor = value); }
        }

        public LineCap StrokeLineCap
        {
            set { _commands.Add(canvas => canvas.StrokeLineCap = value); }
        }

        public LineJoin StrokeLineJoin
        {
            set { _commands.Add(canvas => canvas.StrokeLineJoin = value); }
        }

        public double[] StrokeDashPattern
        {
            set { _commands.Add(canvas => canvas.StrokeDashPattern = value); }
        }

        public Color FillColor
        {
            set { _commands.Add(canvas => canvas.FillColor = value); }
        }

        public Color FontColor
        {
            set { _commands.Add(canvas => canvas.FontColor = value); }
        }

        public string FontName
        {
            set { _commands.Add(canvas => canvas.FontName = value); }
        }

        public double FontSize
        {
            set { _commands.Add(canvas => canvas.FontSize = value); }
        }

        public double Alpha
        {
            set { _commands.Add(canvas => canvas.Alpha = value); }
        }

        public bool Antialias
        {
            set
            {
                // Do nothing, not currently supported in a picture.
            }
        }

        public BlendMode BlendMode
        {
            set { _commands.Add(canvas => canvas.BlendMode = value); }
        }

        public void SubtractFromClip(double x, double y, double width, double height)
        {
            _commands.Add(canvas => canvas.SubtractFromClip(x, y, width, height));
        }

        public void DrawLine(double x1, double y1, double x2, double y2)
        {
            _commands.Add(
                canvas
                    => canvas.DrawLine(x1, y1, x2, y2)
            );
        }

        public void DrawArc(double x, double y, double width, double height, double startAngle, double endAngle, bool clockwise, bool closed)
        {
            _commands.Add(canvas => canvas.DrawArc(x, y, width, height, startAngle, endAngle, clockwise, closed));
        }

        public void FillArc(double x, double y, double width, double height, double startAngle, double endAngle, bool clockwise)
        {
            _commands.Add(canvas => canvas.FillArc(x, y, width, height, startAngle, endAngle, clockwise));
        }

        public void DrawRectangle(double x, double y, double width, double height)
        {
            _commands.Add(canvas => canvas.DrawRectangle(x, y, width, height));
        }

        public void FillRectangle(double x, double y, double width, double height)
        {
            _commands.Add(canvas => canvas.FillRectangle(x, y, width, height));
        }

        public void DrawRoundedRectangle(double x, double y, double width, double height, double cornerRadius)
        {
            _commands.Add(canvas => canvas.DrawRoundedRectangle(x, y, width, height, cornerRadius));
        }

        public void FillRoundedRectangle(double x, double y, double width, double height, double cornerRadius)
        {
            _commands.Add(canvas => canvas.FillRoundedRectangle(x, y, width, height, cornerRadius));
        }

        public void DrawEllipse(double x, double y, double width, double height)
        {
            _commands.Add(canvas => canvas.DrawEllipse(x, y, width, height));
        }

        public void FillEllipse(double x, double y, double width, double height)
        {
            _commands.Add(canvas => canvas.FillEllipse(x, y, width, height));
        }

        public void DrawString(string value, double x, double y, HorizontalAlignment horizontalAlignment)
        {
            _commands.Add(canvas => canvas.DrawString(value, x, y, horizontalAlignment));
        }

        public void DrawString(string value,double x, double y, double width, double height, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment,
            TextFlow textFlow = TextFlow.ClipBounds, double lineSpacingAdjustment = 0)
        {
            _commands.Add(canvas => canvas.DrawString(value, x, y, width, height, horizontalAlignment, verticalAlignment, textFlow, lineSpacingAdjustment));
        }

        public void DrawText(IAttributedText value, double x, double y, double width, double height)
        {
            _commands.Add(canvas => canvas.DrawText(value, x, y, width, height));
        }

        public void DrawPath(Path path)
        {
            _commands.Add(canvas => canvas.DrawPath(path));
        }

        public void FillPath(Path path, WindingMode windingMode)
        {
            _commands.Add(canvas => canvas.FillPath(path, windingMode));
        }

        public void ClipPath(Path path, WindingMode windingMode = WindingMode.NonZero)
        {
            _commands.Add(canvas => canvas.ClipPath(path, windingMode));
        }

        public void ClipRectangle(
            double x,
            double y,
            double width,
            double height)
        {
            _commands.Add(canvas => canvas.ClipRectangle(x, y, width, height));
        }

        public void Rotate(double degrees, double x, double y)
        {
            _commands.Add(canvas => canvas.Rotate(degrees, x, y));
        }

        public void Rotate(double degrees)
        {
            _commands.Add(canvas => canvas.Rotate(degrees));
        }

        public void Scale(double sx, double sy)
        {
            _commands.Add(canvas => canvas.Scale(sx, sy));
        }

        public void Translate(double tx, double ty)
        {
            _commands.Add(canvas => canvas.Translate(tx, ty));
        }

        public void ConcatenateTransform(AffineTransform transform)
        {
            _commands.Add(canvas => canvas.ConcatenateTransform(transform));
        }

        public void SaveState()
        {
            _commands.Add(canvas => canvas.SaveState());
        }

        public bool RestoreState()
        {
            _commands.Add(canvas => canvas.RestoreState());
            return true;
        }

        public void ResetState()
        {

        }

        public void SetShadow(Size offset, double blur, Color color)
        {
            _commands.Add(canvas => canvas.SetShadow(offset, blur, color));
        }

        public void SetFillPaint(Paint paint, Point point1, Point point2)
        {
            _commands.Add(canvas => canvas.SetFillPaint(paint, point1, point2));
        }

        public void SetFillPaint(Paint paint, RectangleF rectangle)
        {
            _commands.Add(canvas => canvas.SetFillPaint(paint, rectangle));
        }

        public void SetFillPaint(Paint paint, double x1, double y1, double x2, double y2)
        {
            _commands.Add(canvas => canvas.SetFillPaint(paint, x1, y1, x2, y2));
        }

        public void SetToSystemFont()
        {
            _commands.Add(canvas => canvas.SetToSystemFont());
        }

        public void SetToBoldSystemFont()
        {
            _commands.Add(canvas => canvas.SetToBoldSystemFont());
        }

        public void DrawImage(IImage image, double x, double y, double width, double height)
        {
            _commands.Add(canvas => canvas.DrawImage(image, x, y, width, height));
        }
    }
}