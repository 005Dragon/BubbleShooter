namespace Code
{
    public class BorderLine
    {
        public Orientation Orientation { get; }
        public float Position { get; }

        public BorderLine(Orientation orientation, float position)
        {
            Orientation = orientation;
            Position = position;
        }
    }
}