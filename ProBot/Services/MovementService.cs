using System;
using System.Collections.Generic;

namespace ProBot
{
    public class MovementService
    {
        public bool ExecuteInstructions(List<Instruction> instructions)
        {
            bool isSuccess;

            if (instructions.Count == 0)
            {
                Message.NeverPlacedOnTable();
                isSuccess = false;
            }
            else
            {
                Move(instructions);
                isSuccess = true;
            }

            return isSuccess;
        }

        public void Move(List<Instruction> instructions)
        {
            var currentHorizontal = 0;
            var currentVertical = 0;
            var currentDirection = new Direction();

            int nextHorizontal = 0;
            int nextVertical = 0;

            bool isIllegal = false;
            bool isOnTable = false;

            foreach (var instruction in instructions)
            {
                if(instruction.Type == InstructionType.PLACE)
                {
                    //Check if starting placement is outside the table
                    isIllegal = CheckForIllegalMove(instruction.LastPlacement.Horizontal, instruction.LastPlacement.Vertical);

                    if (isIllegal)
                    {
                        isOnTable = false;
                        Message.PlacedOutsideTable(currentHorizontal, currentVertical);
                        return;
                    }
                    else
                    {
                        isOnTable = true;
                        currentHorizontal = instruction.LastPlacement.Horizontal;
                        currentVertical = instruction.LastPlacement.Vertical;
                        currentDirection = instruction.Direction;
                    }
                }

                else if (isOnTable && instruction.Type == InstructionType.MOVE)
                {
                    if (currentDirection == Direction.NORTH)
                    {
                        nextHorizontal = currentHorizontal;
                        nextVertical = currentVertical + 1;

                        isIllegal = CheckForIllegalMove(nextHorizontal, nextVertical);
                    }
                    else if (currentDirection == Direction.EAST)
                    {
                        nextHorizontal = currentHorizontal + 1;
                        nextVertical = currentVertical;

                        isIllegal = CheckForIllegalMove(nextHorizontal, nextVertical);
                    }
                    else if (currentDirection == Direction.SOUTH)
                    {
                        nextHorizontal = currentHorizontal;
                        nextVertical = currentVertical - 1;

                        isIllegal = CheckForIllegalMove(nextHorizontal, nextVertical);
                    }
                    else if (currentDirection == Direction.WEST)
                    {
                        nextHorizontal = currentHorizontal - 1;
                        nextVertical = currentVertical;

                        isIllegal = CheckForIllegalMove(nextHorizontal, nextVertical);
                    }
                    else
                    {
                        Message.IllegalDirection();
                        continue;
                    }

                    if (isIllegal)
                    {
                        Message.OutOfBounds(nextHorizontal, nextVertical);
                    }
                    else
                    {
                        currentHorizontal = nextHorizontal;
                        currentVertical = nextVertical;
                    }
                }

                else if (isOnTable && instruction.Type == InstructionType.LEFT || isOnTable && instruction.Type == InstructionType.RIGHT)
                {
                    currentDirection = Turn(instruction.Type, currentDirection);
                }

                else if(isOnTable && instruction.Type == InstructionType.REPORT)
                {
                    Message.PrintReport(currentHorizontal, currentVertical, currentDirection);
                }
            }
        }

        public bool CheckForIllegalMove(int horizontal, int vertical)
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
