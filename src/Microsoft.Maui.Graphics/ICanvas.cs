using Microsoft.Maui.Graphics.Text;

namespace Microsoft.Maui.Graphics
{
    public interface ICanvas
    {
        public double DisplayScale { get; set; }
        public double RetinaScale { get; set; }

        public  double StrokeSize { set; }
        public  double MiterLimit { set; }
        public  Color StrokeColor { set; }
        public  LineCap StrokeLineCap { set; }
        public  LineJoin StrokeLineJoin { set; }
        public  double[] StrokeDashPattern { set; }
        public  Color FillColor { set; }
        public  Color FontColor { set; }
        public  string FontName { set; }
        public  double FontSize { set; }
        public  double Alpha { set; }
        public  bool Antialias { set; }
        public  BlendMode BlendMode { set; }

        public  void DrawPath(Path path);

        public  void FillPath(Path path, WindingMode windingMode);

        public  void SubtractFromClip(double x, double y, double width, double height);

        public  void ClipPath(Path path, WindingMode windingMode = WindingMode.NonZero);

        public  void ClipRectangle(double x, double y, double width, double height);

        public  void DrawLine(double x1, double y1, double x2, double y2);

        public  void DrawArc(double x, double y, double width, double height, double startAngle, double endAngle, bool clockwise, bool closed);

        public  void FillArc(double x, double y, double width, double height, double startAngle, double endAngle, bool clockwise);

        public  void DrawRectangle(double x, double y, double width, double height);

        public  void FillRectangle(double x, double y, double width, double height);

        public  void DrawRoundedRectangle(double x, double y, double width, double height, double cornerRadius);

        public  void FillRoundedRectangle(double x, double y, double width, double height, double cornerRadius);

        public  void DrawEllipse(double x, double y, double width, double height);

        public  void FillEllipse(double x, double y, double width, double height);

        public  void DrawString(string value, double x, double y, HorizontalAlignment horizontalAlignment);

        public void DrawString(
            string value,
            double x,
            double y,
            double width,
            double height,
            HorizontalAlignment horizontalAlignment,
            VerticalAlignment verticalAlignment,
            TextFlow textFlow = TextFlow.ClipBounds,
            double lineSpacingAdjustment = 0);

        public  void DrawText(
            IAttributedText value,
            double x,
            double y,
            double width,
            double height);

        public  void Rotate(double degrees, double x, double y);

        public  void Rotate(double degrees);

        public  void Scale(double sx, double sy);

        public  void Translate(double tx, double ty);

        public  void ConcatenateTransform(AffineTransform transform);

        public  void SaveState();

        public  bool RestoreState();

        public void ResetState();

        public  void SetShadow(Size offset, double blur, Color color);

        public  void SetFillPaint(Paint paint, double x1, double y1, double x2, double y2);

        public  void SetToSystemFont();

        public  void SetToBoldSystemFont();

        public  void DrawImage(IImage image, double x, double y, double width, double height);
    }
}