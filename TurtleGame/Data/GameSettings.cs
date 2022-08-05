namespace TurtleGame.Data
{
    using TurtleGame.Entities;

    public class GameSettings
    {
        public int BoardHorizontalSize { get; set; }

        public int BoardVerticalSize { get; set; }

        public int InnitialTurtleHorPosition { get; set; }

        public int InnitialTurtleVerPosition { get; set; }

        public Direction InnitialTurtleDirection { get; set; }

        public int ExitHorPosition { get; set; }

        public int ExitVerPosition { get; set; }

        public List<Mine> Mines { get; set; } = new List<Mine>();
    }
}
