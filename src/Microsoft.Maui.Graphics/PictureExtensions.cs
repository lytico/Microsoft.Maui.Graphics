namespace Microsoft.Maui.Graphics
{
    public static class PictureExtensions
    {
        public static Rectangle GetBounds(this IPicture target)
        {
            if (target == null) return default;
            return new Rectangle(target.X, target.Y, target.Width, target.Height);
        }
    }
}