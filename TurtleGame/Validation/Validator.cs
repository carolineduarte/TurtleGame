namespace TurtleGame.Validation
{
    using TurtleGame.Data;
    using TurtleGame.Entities;

    public class Validator
    {
        private readonly List<string> errors = new();

        public List<string> ValidateSettings(GameSettings gameSettings)
        {
            if (IsBoardSizeValid(gameSettings))
            {
                if (IsTurtlePositionValid(
                    gameSettings.BoardHorizontalSize,
                    gameSettings.BoardVerticalSize,
                    gameSettings.InnitialTurtleHorPosition,
                    gameSettings.InnitialTurtleVerPosition,
                    gameSettings.InnitialTurtleDirection))
                {
                    if (IsExitPositionValid(gameSettings))
                    {
                        _ = AreMinePositionsValids(gameSettings);
                    }
                }
            }

            return errors;
        }

        public bool IsBoardSizeValid(GameSettings gameSettings)
        {
            var x = gameSettings.BoardHorizontalSize;
            var y = gameSettings.BoardVerticalSize;
            var minesQuantity = gameSettings.Mines.Count;

            if (minesQuantity > 0)
            {
                var elementsQuantity = 2 + minesQuantity;

                if (x == 0 && y < elementsQuantity - 1
                    || x < elementsQuantity - 1 && y == 0
                    || x > 0 && y > 0 && x * y < elementsQuantity)
                {
                    errors.Add($"The board must have at least {elementsQuantity} cells. Verify the quantity of lines and columns informed.");

                    return false;
                }

                if (x < 0 || y < 0)
                {
                    errors.Add("The quantity of lines and/or columns is invalid.");

                    return false;
                }

                return true;
            }

            errors.Add("There must be at least one mine.");

            return false;
        }

        public bool IsTurtlePositionValid(int boardX, int boardY, int turtleX, int turtleY)
        {
            if (turtleX < 0 || turtleY < 0 || turtleX > boardX || turtleY > boardY)
            {
                errors.Add("The position of the Turtle is invalid.");

                return false;
            }

            return true;
        }

        public bool IsTurtlePositionValid(int boardX, int boardY, int turtleX, int turtleY, Direction direction)
        {
            if (direction != Direction.None)
            {
                IsTurtlePositionValid(boardX, boardY, turtleX, turtleY);
            }

            return false;
        }

        public bool IsExitPositionValid(GameSettings gameSettings)
        {
            var boardX = gameSettings.BoardHorizontalSize;
            var boardY = gameSettings.BoardVerticalSize;
            var turtleX = gameSettings.InnitialTurtleHorPosition;
            var turtleY = gameSettings.InnitialTurtleVerPosition;
            var exitX = gameSettings.ExitHorPosition;
            var exitY = gameSettings.ExitVerPosition;

            if (exitX < 0 || exitY < 0 || exitX > boardX || exitY > boardY || exitX == turtleX && exitY == turtleY)
            {
                errors.Add("The position of the Exit is invalid.");

                return false;
            }

            return true;
        }

        public bool AreMinePositionsValids(GameSettings gameSettings)
        {
            var boardX = gameSettings.BoardHorizontalSize;
            var boardY = gameSettings.BoardVerticalSize;
            var turtleX = gameSettings.InnitialTurtleHorPosition;
            var turtleY = gameSettings.InnitialTurtleVerPosition;
            var exitX = gameSettings.ExitHorPosition;
            var exitY = gameSettings.ExitVerPosition;
            var mines = gameSettings.Mines;

            for (int i = 0; i < mines.Count; i++)
            {
                for (int j = i; j < mines.Count - 1; j++)
                {
                    if (mines.ElementAt(i).HorizontalPosition == mines.ElementAt(j + 1).HorizontalPosition
                    && mines.ElementAt(i).VerticalPosition == mines.ElementAt(j + 1).VerticalPosition)
                    {
                        errors.Add($"The position of the Mine {i + 1} is equal to the Mine {j + 2}.");

                        return false;
                    }
                }
            }

            foreach (var m in mines)
            {
                if (!IsMinePositionValid(
                    boardX,
                    boardY,
                    turtleX,
                    turtleY,
                    exitX,
                    exitY,
                    m.HorizontalPosition,
                    m.VerticalPosition,
                    m.MineId))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsMinePositionValid(int boardX, int boardY, int turtleX, int turtleY, int exitX, int exitY, int mineX, int mineY, int id)
        {
            if (mineX < 0 || mineY < 0 || mineX > boardX || mineY > boardY
                || mineX == turtleX && mineY == turtleY
                || mineX == exitX && mineY == exitY)
            {
                errors.Add($"The position of the Mine {id} is invalid.");

                return false;
            }

            return true;
        }
    }
}
