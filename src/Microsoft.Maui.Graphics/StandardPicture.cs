namespace Microsoft.Maui.Graphics
{
    public delegate void DrawingCommand(ICanvas canvas);

    public class StandardPicture : IPicture
    {
        private readonly DrawingCommand[] _commands;

        public double X { get; }
        public double Y { get; }
        public double Width { get; }
        public double Height { get; }
        public string Hash { get; set; }

        public StandardPicture(double x, double y, double width, double height, DrawingCommand[] commands, string hash = null)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            _commands = commands;

            Hash = hash;
        }

        public void Draw(ICanvas canvas)
        {
            if (_commands != null)
                foreach (var command in _commands)
                    command.Invoke(canvas);
        }
    }
}