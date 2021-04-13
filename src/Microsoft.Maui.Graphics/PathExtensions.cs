using System.Globalization;
using System.IO;

namespace Microsoft.Maui.Graphics
{
    public static class PathExtensions
    {
        public static string ToDefinitionString(this Path path, double ppu = 1)
        {
            var writer = new StringWriter();

            for (var i = 0; i < path.OperationCount; i++)
            {
                var type = path.GetSegmentType(i);
                var points = path.GetPointsForSegment(i);

                if (type == PathOperation.Move)
                {
                    writer.Write("M");
                    WritePoint(writer, points[0], ppu);
                }
                else if (type == PathOperation.Line)
                {
                    writer.Write(" L");
                    WritePoint(writer, points[0], ppu);
                }
                else if (type == PathOperation.Quad)
                {
                    writer.Write(" Q");
                    WritePoint(writer, points[0], ppu);
                    writer.Write(" ");
                    WritePoint(writer, points[1], ppu);
                }
                else if (type == PathOperation.Cubic)
                {
                    writer.Write(" C");
                    WritePoint(writer, points[0], ppu);
                    writer.Write(" ");
                    WritePoint(writer, points[1], ppu);
                    writer.Write(" ");
                    WritePoint(writer, points[2], ppu);
                }
                else if (type == PathOperation.Close)
                {
                    writer.Write(" Z ");
                }
            }

            return writer.ToString();
        }

        private static void WritePoint(StringWriter writer, Point point, double ppu)
        {
            double x = point.X * ppu;
            double y = point.Y * ppu;

            string cx = x.ToString(CultureInfo.InvariantCulture);
            string cy = y.ToString(CultureInfo.InvariantCulture);

            writer.Write(cx);
            writer.Write(" ");
            writer.Write(cy);
        }

        public static Path AsScaledPath(
            this Path target,
            double scale)
        {
            var scaledPath = new Path(target);
            var transform = AffineTransform.GetScaleInstance(scale, scale);
            scaledPath.Transform(transform);
            return scaledPath;
        }
    }
}