namespace TurtleGame.Entities
{
    public class Exit : BoardElement
    {
        private readonly string elementName = "Exit";

        public Exit(int x, int y) : base(x, y)
        {           
            this.HorizontalPosition = x;
            this.VerticalPosition = y;
            this.ElementName = elementName;
        }
    }
}
