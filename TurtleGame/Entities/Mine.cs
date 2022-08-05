namespace TurtleGame.Entities
{
    public class Mine : BoardElement
    {
        public int MineId { get; set; }

        private readonly string elementName = "Mine";

        public Mine(int mineId, int x, int y) : base (x, y)
        {
            this.MineId = mineId;
            this.HorizontalPosition = x;
            this.VerticalPosition = y;
            this.ElementName = elementName;
        }
    }
}
