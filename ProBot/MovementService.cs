using System.Collections.Generic;

namespace ProBot
{
    public class MovementService
    {
        public void Move(List<Instruction> instructions)
        {
            var startingPlacement = instructions[0];

            var currentVertical = startingPlacement.Position.Vertical;
            var currentHorizontal = startingPlacement.Position.Horizontal;
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
                        Message.OutOfBounds(nextVertical, nextHorizontal);
                        continue;
                    }

                    if (!isIllegal)
                    {
                        currentVertical = nextVertical;
                        currentHorizontal = nextHorizontal;
                        continue;
                    }
                }

                if (instruction.Type == InstructionType.REPORT)
                {
                    Message.PrintReport(currentVertical, currentHorizontal, currentDirection);
                    continue;
                }

                if (instruction.Type == InstructionType.LEFT || instruction.Type == InstructionType.RIGHT)
                {
                    currentDirection = Turn(instruction.Type, currentDirection);
                    continue;
                }
            }
        }

        public bool CheckForIllegalMove(int vertical, int horizontal)
        {
            if (horizontal > 5 || horizontal < 0 || vertical > 5 || vertical < 0)
            {
                //Message.OutOfBounds(vertical, horizontal);
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
