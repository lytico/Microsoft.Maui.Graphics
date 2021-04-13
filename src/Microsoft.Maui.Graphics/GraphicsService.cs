using System.IO;

namespace Microsoft.Maui.Graphics
{
    public delegate void LayoutLine(Point aPoint, ITextAttributes aTextual, string aText, double aAscent, double aDescent, double aLeading);

    public interface IGraphicsService
    {
        string SystemFontName { get; }
        string BoldSystemFontName { get; }

        Size GetStringSize(string value, string fontName, double textSize);
        Size GetStringSize(string value, string fontName, double textSize, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment);

        IImage LoadImageFromStream(Stream stream, ImageFormat format = ImageFormat.Png);
        BitmapExportContext CreateBitmapExportContext(int width, int height, double displayScale = 1);
    }
}