namespace TurtleGame.Engine
{
    using TurtleGame.Data;
    using TurtleGame.Entities;
    using TurtleGame.Validation;

    public class Game
    {
        private readonly ReadFiles readFiles;

        private readonly Validator validator;

        private readonly MoveProcessor moveProcessor;

        private Board board;

        public Game()
        {
            readFiles = new ReadFiles();
            validator = new Validator();
            moveProcessor = new MoveProcessor();
            board = new Board();
        }

        public List<string> InnitializeBoard(string settingsFile, string movesFile)
        {
            var settings = readFiles.ReadSettings(settingsFile);

            var errors = validator.ValidateSettings(settings);

            if (errors.Any())
            {
                return errors;
            }

            board.BoardHorizontalSize = settings.BoardHorizontalSize;
            board.BoardVerticalSize = settings.BoardVerticalSize;
            board.Grid = new BoardElement[settings.BoardHorizontalSize, settings.BoardVerticalSize];

            PopulateBoard(board, settings);

            var moves = readFiles.ReadMovements(movesFile);

            return PlayMoves(moves);
        }

        private void PopulateBoard(Board board, GameSettings settings)
        {
            var turtle = new Turtle(settings.InnitialTurtleHorPosition, settings.InnitialTurtleVerPosition, settings.InnitialTurtleDirection);
            this.board.BoardElementsList.Add(turtle);
            this.board.Grid![turtle.HorizontalPosition, turtle.VerticalPosition] = turtle;

            var exit = new Exit(settings.ExitHorPosition, settings.ExitVerPosition);
            this.board.BoardElementsList.Add(exit);
            this.board.Grid[exit.HorizontalPosition, exit.VerticalPosition] = exit;

            foreach (var mine in settings.Mines)
            {
                this.board.BoardElementsList.Add(mine);
                this.board.Grid[mine.HorizontalPosition, mine.VerticalPosition] = mine;
            }
        }

        private List<string> PlayMoves(GameMovements moves)
        {
            var messages = new List<string>();

            int i = 1;

            if (moves.TurtleMovements.Any())
            {
                foreach (var move in moves.TurtleMovements)
                {
                    var currentTurtle = board.BoardElementsList.Find(e => e.ElementName!.Equals("Turtle"));

                    var (updatedTurtle, IsUpdated) = moveProcessor.MakeTurtleMovement(currentTurtle!, move);

                    if (IsUpdated)
                    {
                        if (validator.IsTurtlePositionValid(
                        board.BoardHorizontalSize,
                        board.BoardVerticalSize,
                        updatedTurtle.HorizontalPosition,
                        updatedTurtle.VerticalPosition))
                        {
                            messages.Add($"Sequence {i}: {FindBoardElementTypeAndRetriveMessage(updatedTurtle)} {TurtlePosition(updatedTurtle)} ");

                            UpdateBoard(currentTurtle!, updatedTurtle);
                        }
                        else
                        {
                            messages.Add($"Sequence {i}: {Messages.NotValid} {Messages.NoUpdate} {TurtlePosition(currentTurtle!)}");
                        }
                    }
                    else
                    {
                        messages.Add($"Sequence {i}: {Messages.NoUpdate} {TurtlePosition(currentTurtle!)}");
                    }

                    i++;
                }
            }

            return messages;
        }

        private string FindBoardElementTypeAndRetriveMessage(BoardElement turtle)
        {
            var element = board.Grid![turtle.HorizontalPosition, turtle.VerticalPosition];

            if (element == null)
            {
                return $"{Messages.Update} {Messages.Danger}";
            }
            else
            {
                if (element.ElementName == "Mine")
                {
                    return Messages.Mine;
                }

                if (element.ElementName == "Exit")
                {
                    return $"{Messages.Success} {Messages.Exit}";
                }

                return $"{Messages.Success} {Messages.Update}";
            }

        }

        private void UpdateBoard(BoardElement currentTurtle, BoardElement updatedTurtle)
        {
            board.BoardElementsList[0] = updatedTurtle;
            board.Grid![updatedTurtle.HorizontalPosition, updatedTurtle.VerticalPosition] = updatedTurtle;
            board.Grid[currentTurtle!.HorizontalPosition, currentTurtle!.VerticalPosition] =
                new BoardElement(currentTurtle.HorizontalPosition, currentTurtle.VerticalPosition);
        }

        private string TurtlePosition(BoardElement turtle)
        {
            return $"Turtle Position ({turtle.HorizontalPosition}, {turtle.VerticalPosition}, {turtle.ElementDirection})";
        }
    }
}
