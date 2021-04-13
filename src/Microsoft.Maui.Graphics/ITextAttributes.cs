namespace Microsoft.Maui.Graphics
{
    public interface ITextAttributes
    {
        string FontName { get; set; }

        double FontSize { get; set; }

        double Margin { get; set; }

        Color TextFontColor { get; set; }

        HorizontalAlignment HorizontalAlignment { get; set; }

        VerticalAlignment VerticalAlignment { get; set; }
    }
}