namespace Microsoft.Maui.Graphics
{
    public class StandardTextAttributes : ITextAttributes
    {
        public string FontName { get; set; }

        public double FontSize { get; set; }

        public HorizontalAlignment HorizontalAlignment { get; set; }

        public double Margin { get; set; }

        public Color TextFontColor { get; set; }

        public VerticalAlignment VerticalAlignment { get; set; }
    }
}