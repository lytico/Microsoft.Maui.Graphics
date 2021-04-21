using Microsoft.Maui.Graphics;
using Cairo;

namespace Microsoft.Maui.Graphics.Native.Gtk {

    public static class GtkGraphicsExtensions {

        public static Cairo.Color ToCairoColor (this Color col)
        {
            return new Cairo.Color (col.Red, col.Green, col.Blue, col.Alpha);
        }

        public static Cairo.Color ToCairoColor (this Gdk.RGBA color)
        {
            return new Cairo.Color (color.Red, color.Green, color.Blue, color.Alpha);
        }

        public static Gdk.Color ToGdkColor (this Color color)
        {
            return new Gdk.Color ((byte)(color.Red * 255), (byte)(color.Green * 255), (byte)(color.Blue * 255));
        }

        public static Color ToColor (this Gdk.Color color)
        {
            return new Color ((float)color.Red / (float)ushort.MaxValue, (float)color.Green / (float)ushort.MaxValue, (float)color.Blue / (float)ushort.MaxValue);
        }
    }

}