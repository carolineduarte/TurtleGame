namespace TurtleGame.Entities
{
    public class Turtle : BoardElement
    {
        private readonly string elementName = "Turtle";

        public Turtle(int x, int y, Direction direction) : base(x, y)
        {
            this.HorizontalPosition = x;
            this.VerticalPosition = y;
            this.ElementDirection = direction;
            this.ElementName = elementName;
        }
    }
}
