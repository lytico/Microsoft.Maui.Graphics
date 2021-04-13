using System;

namespace Microsoft.Maui.Graphics
{
    public class CanvasState : IDisposable
    {
        public double[] StrokeDashPattern { get; set; }
        public double StrokeSize { get; set; } = 1;
        public double Scale { get; set; } = 1;
        public AffineTransform Transform { get; set; }

        protected CanvasState()
        {
            Transform = new AffineTransform();
        }

        protected CanvasState(CanvasState prototype)
        {
            StrokeDashPattern = prototype.StrokeDashPattern;
            StrokeSize = prototype.StrokeSize;
            Transform = new AffineTransform(prototype.Transform);
            Scale = prototype.Scale;
        }

        public virtual void Dispose()
        {
            // Do nothing right now
        }
    }
}