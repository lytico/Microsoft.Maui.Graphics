using System;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.Maui.Graphics
{
    public enum ResizeMode
    {
        Fit,
        Bleed,
        Stretch
    }

    public interface IImage : IDrawable, IDisposable
    {
        double Width { get; }
        double Height { get; }
        IImage Downsize(double maxWidthOrHeight, bool disposeOriginal = false);
        IImage Downsize(double maxWidth, double maxHeight, bool disposeOriginal = false);
        IImage Resize(double width, double height, ResizeMode resizeMode = ResizeMode.Fit, bool disposeOriginal = false);
        void Save(Stream stream, ImageFormat format = ImageFormat.Png, double quality = 1);
        Task SaveAsync(Stream stream, ImageFormat format = ImageFormat.Png, double quality = 1);
    }
}