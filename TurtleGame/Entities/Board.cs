namespace TurtleGame.Entities
{
    public class Board
    {
        public int BoardHorizontalSize { get; set; }

        public int BoardVerticalSize { get; set; }

        public BoardElement[,]? Grid { get; set; }

        public List<BoardElement> BoardElementsList { get; set; } = new List<BoardElement>();
    }
}