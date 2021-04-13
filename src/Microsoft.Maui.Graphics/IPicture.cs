namespace Microsoft.Maui.Graphics
{
    public interface IPicture
    {
        void Draw(ICanvas canvas);

        double X { get; }

        double Y { get; }

        double Width { get; }

        double Height { get; }
    }
}