namespace TurtleGame.Data
{
    using TurtleGame.Entities;

    public class ReadFiles
    {
        public GameSettings ReadSettings(string settings)
        {
            var gameSettings = new GameSettings();

            string[] lines = File.ReadAllLines(settings);

            var id = 1;

            foreach (var line in lines)
            {
                if (line.StartsWith("Board", StringComparison.InvariantCultureIgnoreCase))
                {
                    var (x, y, _) = ReadValues(line);
                    gameSettings.BoardHorizontalSize = x;
                    gameSettings.BoardVerticalSize = y;
                }

                if (line.StartsWith("Turtle", StringComparison.InvariantCultureIgnoreCase))
                {
                    var (x, y, d) = ReadValues(line);
                    gameSettings.InnitialTurtleHorPosition = x;
                    gameSettings.InnitialTurtleVerPosition = y;
                    gameSettings.InnitialTurtleDirection = d;
                }

                if (line.StartsWith("Exit", StringComparison.InvariantCultureIgnoreCase))
                {
                    var (x, y, _) = ReadValues(line);
                    gameSettings.ExitHorPosition = x;
                    gameSettings.ExitVerPosition = y;
                }

                if (line.StartsWith("Mine", StringComparison.InvariantCultureIgnoreCase))
                {
                    var (x, y, _) = ReadValues(line);
                    var mine = new Mine(id, x, y);
                    gameSettings.Mines.Add(mine);

                    id++;
                }
            }

            return gameSettings;
        }

        public GameMovements ReadMovements(string moves)
        {
            var gameMovements = new GameMovements();

            string movements = File.ReadAllText(moves);

            string[] values = movements.Split(',');

            foreach (var value in values)
            {
                var movement = DetermineMovement(value);

                gameMovements.TurtleMovements.Add(movement);
            }

            return gameMovements;
        }

        private static (int x, int y, Direction direction) ReadValues(string line)
        {
            var index = line.IndexOf(": ");

            string[] values = line.Substring(index + 2).Split(',');

            int x = values.Length >= 2
                ? int.Parse(values[0])
                : -1;
            int y = values.Length >= 2
                ? int.Parse(values[1])
                : -1;
            Direction direction = values.Length > 2
                ? DetermineDirection(values[2].ToUpper())
                : Direction.None;

            return (x, y, direction);
        }

        private static Direction DetermineDirection(string dir)
        {
            switch (dir.Trim())
            {
                case "N": return Direction.North;
                case "E": return Direction.East;
                case "S": return Direction.South;
                case "W": return Direction.West;
                default: return Direction.None;
            }
        }

        private static Movement DetermineMovement(string move)
        {
            switch (move.Trim())
            {
                case "M": return Movement.MoveForward;
                case "R": return Movement.Rotate90DegClockwise;
                default: return Movement.None;
            }
        }
    }
}
