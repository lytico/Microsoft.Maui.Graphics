using System;

namespace Microsoft.Maui.Graphics
{
    public class GradientStop : IComparable<GradientStop>
    {
        private Color _color;
        private double _offset;

        public GradientStop(double offset, Color color)
        {
            _color = color;
            _offset = offset;
        }

        public GradientStop(GradientStop source)
        {
            _color = source._color;
            _offset = source._offset;
        }

        public Color Color
        {
            get => _color;
            set => _color = value;
        }

        public double Offset
        {
            get => _offset;
            set => _offset = value;
        }

        public int CompareTo(GradientStop obj)
        {
            if (_offset < obj._offset)
                return -1;
            if (_offset > obj._offset)
                return 1;

            return 0;
        }
    }
}