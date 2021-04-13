namespace Microsoft.Maui.Graphics
{
    public interface IPdfExportService
    {
        PdfExportContext CreateContext(double width = -1, double height = -1);
    }
}