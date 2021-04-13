namespace Microsoft.Maui.Graphics
{
    public abstract class AbstractPattern : IPattern
    {
        public double Width { get; }
        public double Height { get; }
        public double StepX { get; }
        public double StepY { get; }

        protected AbstractPattern(double width, double height, double stepX, double stepY)
        {
            Width = width;
            Height = height;
            StepX = stepX;
            StepY = stepY;
        }

        protected AbstractPattern(double width, double height) : this(width, height, width, height)
        {
        }

        protected AbstractPattern(double stepSize) : this(stepSize, stepSize)
        {
        }

        public abstract void Draw(ICanvas canvas);
    }
}