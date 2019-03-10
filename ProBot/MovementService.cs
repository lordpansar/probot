namespace ProBot
{
    public class MovementService
    {
        public void Move(Instruction instructions)
        {
            var currentVertical = instructions.StartPosition.Vertical;
            var currentHorizontal = instructions.StartPosition.Horizontal;
            var currentDirection = instructions.Direction;

            int nextVertical = 0;
            int nextHorizontal = 0;

            bool isIllegal;

            //Check if placement is outside the table
            isIllegal = CheckForIllegalMove(currentVertical, currentHorizontal);

            if (isIllegal)
            {
                return;
            }

            foreach (var instruction in instructions.InstructionsList)
            {
                if (instruction == "MOVE")
                {
                    if (currentDirection == Direction.NORTH)
                    {
                        nextVertical = currentVertical - 1;
                        nextHorizontal = currentHorizontal;

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);
                    }
                    else if (currentDirection == Direction.EAST)
                    {
                        nextVertical = currentVertical;
                        nextHorizontal = currentHorizontal + 1;

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);
                    }
                    else if (currentDirection == Direction.SOUTH)
                    {
                        nextVertical = currentVertical + 1;
                        nextHorizontal = currentHorizontal;

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);
                    }
                    else if (currentDirection == Direction.WEST)
                    {
                        nextVertical = currentVertical;
                        nextHorizontal = currentHorizontal - 1;

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);
                    }

                    if (isIllegal)
                    {
                        continue;
                    }

                    if (!isIllegal)
                    {
                        currentVertical = nextVertical;
                        currentHorizontal = nextHorizontal;
                        continue;
                    }
                }

                if (instruction == "REPORT")
                {
                    Message.PrintReport(currentVertical, currentHorizontal, currentDirection);
                    continue;
                }

                if (instruction == "LEFT" || instruction == "RIGHT")
                {
                    currentDirection = Turn(instruction, currentDirection);
                    continue;
                }
            }
        }

        public bool CheckForIllegalMove(int vertical, int horizontal)
        {
            if (horizontal > 5 || horizontal < 0 || vertical > 5 || vertical < 0)
            {
                Message.OutOfBounds(vertical, horizontal);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Direction Turn(string turn, Direction currentDirection)
        {
            Direction newDirection = new Direction();

            if (turn == "LEFT")
            {
                if (currentDirection == Direction.NORTH)
                {
                    newDirection = Direction.WEST;
                }
                if (currentDirection == Direction.EAST)
                {
                    newDirection = Direction.NORTH;
                }
                if (currentDirection == Direction.SOUTH)
                {
                    newDirection = Direction.EAST;
                }
                if (currentDirection == Direction.WEST)
                {
                    newDirection = Direction.SOUTH;
                }
            }
            else
            {
                if (currentDirection == Direction.NORTH)
                {
                    newDirection = Direction.EAST;
                }
                if (currentDirection == Direction.EAST)
                {
                    newDirection = Direction.SOUTH;
                }
                if (currentDirection == Direction.SOUTH)
                {
                    newDirection = Direction.WEST;
                }
                if (currentDirection == Direction.WEST)
                {
                    newDirection = Direction.NORTH;
                }
            }

            return newDirection;
        }
    }
}
