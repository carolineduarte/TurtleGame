namespace TurtleGame.Entities
{
    public class BoardElement : Position
    {
        public string ElementName { get; set; }

        public Direction ElementDirection { get; set; }

        private readonly string elementName = "None";

        public BoardElement(int x, int y) : base(x, y)
        {
            this.HorizontalPosition = x;
            this.VerticalPosition = y;
            this.ElementName = elementName;
        }
    }
}
