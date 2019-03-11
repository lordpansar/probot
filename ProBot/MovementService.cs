using System.Collections.Generic;

namespace ProBot
{
    public class MovementService
    {
        public void Move(List<Instruction> instructions)
        {
            var startingPlacement = instructions[0];

            var currentVertical = startingPlacement.StartPosition.Vertical;
            var currentHorizontal = startingPlacement.StartPosition.Horizontal;
            var currentDirection = startingPlacement.Direction;

            int nextVertical = 0;
            int nextHorizontal = 0;

            bool isIllegal;

            //Check if starting placement is outside the table
            isIllegal = CheckForIllegalMove(currentVertical, currentHorizontal);

            if (isIllegal)
            {
                Message.PlacedOutsideTable(currentVertical, currentHorizontal);
                return;
            }

            foreach (var instruction in instructions)
            {
                if (instruction.Type == InstructionType.MOVE)
                {
                    if (currentDirection == Direction.NORTH)
                    {
                        nextVertical = currentVertical + 1;
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
                        nextVertical = currentVertical - 1;
                        nextHorizontal = currentHorizontal;

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);
                    }
                    else if (currentDirection == Direction.WEST)
                    {
                        nextVertical = currentVertical;
                        nextHorizontal = currentHorizontal - 1;

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);
                    }

                    else if (currentDirection == Direction.ILLEGAL)
                    {
                        Message.IllegalDirection();
                        continue;
                    }

                    if (isIllegal)
                    {
                        Message.OutOfBounds(nextVertical, nextHorizontal);
                        continue;
                    }

                    else if (!isIllegal)
                    {
                        currentVertical = nextVertical;
                        currentHorizontal = nextHorizontal;
                        continue;
                    }
                }

                else if (instruction.Type == InstructionType.REPORT)
                {
                    Message.PrintReport(currentVertical, currentHorizontal, currentDirection);
                    continue;
                }

                else if (instruction.Type == InstructionType.LEFT || instruction.Type == InstructionType.RIGHT)
                {
                    currentDirection = Turn(instruction.Type, currentDirection);
                    continue;
                }
            }
        }

        public bool CheckForIllegalMove(int vertical, int horizontal)
        {
            if (horizontal > 4 || horizontal < 0 || vertical > 4 || vertical < 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Direction Turn(InstructionType turn, Direction currentDirection)
        {
            Direction newDirection = new Direction();

            if (turn == InstructionType.LEFT)
            {
                switch(currentDirection)
                {
                    case Direction.NORTH:
                        newDirection = Direction.WEST;
                        break;
                    case Direction.EAST:
                        newDirection = Direction.NORTH;
                        break;
                    case Direction.SOUTH:
                        newDirection = Direction.EAST;
                        break;
                    case Direction.WEST:
                        newDirection = Direction.SOUTH;
                        break;
                    default:
                        newDirection = Direction.ILLEGAL;
                        break;
                }
            }
            else
            {
                switch (currentDirection)
                {
                    case Direction.NORTH:
                        newDirection = Direction.EAST;
                        break;
                    case Direction.EAST:
                        newDirection = Direction.SOUTH;
                        break;
                    case Direction.SOUTH:
                        newDirection = Direction.WEST;
                        break;
                    case Direction.WEST:
                        newDirection = Direction.NORTH;
                        break;
                    default:
                        newDirection = Direction.ILLEGAL;
                        break;
                }
            }

            return newDirection;
        }
    }
}
