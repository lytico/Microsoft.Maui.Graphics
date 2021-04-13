using System;
using System.IO;
using System.Threading;

namespace Microsoft.Maui.Graphics
{
    public abstract class PdfExportContext : IDisposable
    {
        private readonly double _defaultWidth;
        private readonly double _defaultHeight;

        private double _currentPageWidth;
        private double _currentPageHeight;
        private int _pageCount;

        protected PdfExportContext(
            double defaultWidth = -1,
            double defaultHeight = -1)
        {
            if (defaultWidth <= 0 || defaultHeight <= 0)
            {
                if ("en-US".Equals(Thread.CurrentThread.CurrentCulture.Name))
                {
                    // Letter
                    defaultWidth = 612;
                    defaultHeight = 792;
                }
                else
                {
                    // A4
                    defaultWidth = 595;
                    defaultHeight = 842;
                }
            }

            _defaultWidth = defaultWidth;
            _defaultHeight = defaultHeight;
        }

        public double DefaultWidth => _defaultWidth;

        public double DefaultHeight => _defaultHeight;

        public int PageCount => _pageCount;

        public void AddPage(double width = -1, double height = -1)
        {
            if (width <= 0 || height <= 0)
            {
                _currentPageWidth = _defaultWidth;
                _currentPageHeight = _defaultHeight;
            }
            else
            {
                _currentPageWidth = width;
                _currentPageHeight = height;
            }

            AddPageImpl(_currentPageWidth, _currentPageHeight);
            _pageCount++;
        }

        public virtual void Dispose()
        {
        }

        protected abstract void AddPageImpl(double width, double height);

        public abstract ICanvas Canvas { get; }

        public abstract void WriteToStream(Stream stream);
    }
}