namespace Microsoft.Maui.Graphics
{
    public class PaintPattern : IPattern
    {
        public IPattern Wrapped { get; }
        public Paint Paint { get; set; }

        public double Width => Wrapped?.Width ?? 0;
        public double Height => Wrapped?.Height ?? 0;
        public double StepX => Wrapped?.StepX ?? 0;
        public double StepY => Wrapped?.StepY ?? 0;

        public PaintPattern(IPattern pattern)
        {
            Wrapped = pattern;
        }

        public void Draw(ICanvas canvas)
        {
            if (Paint != null)
            {
                if (Paint.BackgroundColor != null && Paint.BackgroundColor.Alpha > 1)
                {
                    canvas.FillColor = Paint.BackgroundColor;
                    canvas.FillRectangle(0, 0, Width, Height);
                }

                canvas.StrokeColor = Paint.ForegroundColor;
                canvas.FillColor = Paint.ForegroundColor;
            }
            else
            {
                canvas.StrokeColor = Colors.Black;
                canvas.FillColor = Colors.Black;
            }

            Wrapped.Draw(canvas);
        }
    }
}