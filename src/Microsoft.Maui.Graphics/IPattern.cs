namespace Microsoft.Maui.Graphics
{
    public interface IPattern
    {
        double Width { get; }
        double Height { get; }
        double StepX { get; }
        double StepY { get; }
        void Draw(ICanvas canvas);
    }
}