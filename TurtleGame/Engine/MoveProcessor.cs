namespace TurtleGame.Engine
{
    using TurtleGame.Entities;

    public class MoveProcessor
    {
        public (BoardElement updatedTurtle, bool IsPositionChanged) MakeTurtleMovement(BoardElement innitialPosition, Movement movement)
        {
            var finalPosition = new Turtle(
                innitialPosition.HorizontalPosition,
                innitialPosition.VerticalPosition,
                innitialPosition.ElementDirection);

            switch (innitialPosition.ElementDirection)
            {
                case Direction.North:
                    switch (movement)
                    {
                        case Movement.MoveForward:

                            finalPosition.VerticalPosition--;
                            break;

                        case Movement.Rotate90DegClockwise:

                            finalPosition.ElementDirection = Direction.East;
                            break;
                    }
                    break;

                case Direction.East:
                    switch (movement)
                    {
                        case Movement.MoveForward:
                            finalPosition.HorizontalPosition++;
                            break;

                        case Movement.Rotate90DegClockwise:
                            finalPosition.ElementDirection = Direction.South;
                            break;
                    }
                    break;

                case Direction.South:
                    switch (movement)
                    {
                        case Movement.MoveForward:
                            finalPosition.VerticalPosition++;
                            break;

                        case Movement.Rotate90DegClockwise:
                            finalPosition.ElementDirection = Direction.West;
                            break;
                    }
                    break;

                case Direction.West:
                    switch (movement)
                    {
                        case Movement.MoveForward:
                            finalPosition.HorizontalPosition--;
                            break;
                        case Movement.Rotate90DegClockwise:
                            finalPosition.ElementDirection = Direction.North;
                            break;
                    }
                    break;
            }

            if (innitialPosition.HorizontalPosition == finalPosition.HorizontalPosition
                && innitialPosition.VerticalPosition == finalPosition.VerticalPosition
                && innitialPosition.ElementDirection == finalPosition.ElementDirection)
            {
                return (innitialPosition, false);
            }

            return (finalPosition, true);

        }
    }
}
