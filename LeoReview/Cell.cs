namespace LeoReview
{
    public class Cell
    {
        private readonly int _x;
        private readonly int _y;

        public int X => _x;
        public int Y => _y;

        public Cell(int x, int y)
        {
            _x = x;
            _y= y;
        }
    }
}