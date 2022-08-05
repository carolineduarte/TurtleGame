namespace TurtleGame.Entities
{
    public class Position
    {
        public int HorizontalPosition { get; set; }

        public int VerticalPosition { get; set; }        

        public Position(int horizontalPosition, int verticalPosition)
        {
            this.HorizontalPosition = horizontalPosition;
            this.VerticalPosition = verticalPosition;
        }
    }
}
