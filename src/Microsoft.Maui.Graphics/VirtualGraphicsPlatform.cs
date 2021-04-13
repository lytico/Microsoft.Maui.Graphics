using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Maui.Graphics
{
    public class VirtualGraphicsPlatform : IGraphicsService
    {
        public List<Path> ConvertToPaths(Path aPath, string text, ITextAttributes textAttributes, double ppu, double zoom)
        {
            return new List<Path>();
        }

        public Size GetStringSize(string value, string fontName, double textSize)
        {
            return new Size(value.Length * 10, textSize + 2);
        }

        public Size GetStringSize(string value, string fontName, double textSize, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            return new Size(value.Length * 10, textSize + 2);
        }

        public void LayoutText(Path path, string text, ITextAttributes textAttributes, LayoutLine callback)
        {
            // Do nothing
        }

        public Rectangle GetPathBounds(Path path)
        {
            throw new NotImplementedException();
        }

        public Rectangle GetPathBoundsWhenRotated(Point center, Path path, double angle)
        {
            throw new NotImplementedException();
        }

        public bool PathContainsPoint(Path path, Point point, double ppu, double zoom, double strokeWidth)
        {
            throw new NotImplementedException();
        }

        public bool PointIsOnPath(Path path, Point point, double ppu, double zoom, double strokeWidth)
        {
            throw new NotImplementedException();
        }

        public bool PointIsOnPathSegment(Path path, int segmentIndex, Point point, double ppu, double zoom, double strokeWidth)
        {
            throw new NotImplementedException();
        }

        public IImage LoadImageFromStream(Stream stream, ImageFormat format = ImageFormat.Png)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (stream)
                {
                    stream.CopyTo(memoryStream);
                }

                return new VirtualImage(memoryStream.ToArray(), format);
            }
        }

        public BitmapExportContext CreateBitmapExportContext(int width, int height, double displayScale = 1)
        {
            return null;
        }

        public string SystemFontName => "Arial";
        public string BoldSystemFontName => "Arial-Bold";
    }
}