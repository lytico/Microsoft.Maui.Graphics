namespace Microsoft.Maui.Graphics
{
    public static class DrawableExtensions
    {
        public static IImage ToImage(this IDrawable drawable, int width, int height, double scale = 1)
        {
            if (drawable == null) return null;

            using (var context = GraphicsPlatform.CurrentService.CreateBitmapExportContext(width, height))
            {
                context.Canvas.Scale(scale, scale);
                drawable.Draw(context.Canvas, new Rectangle(0, 0, width / scale, height / scale));
                return context.Image;
            }
        }
    }
}