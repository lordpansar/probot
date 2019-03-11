﻿using System.Collections.Generic;

namespace ProBot
{
    public class MovementService
    {
        public void Move(List<Instruction> instructions)
        {
            var startingPlacement = instructions[0];

            var currentHorizontal = startingPlacement.StartPosition.Horizontal;
            var currentVertical = startingPlacement.StartPosition.Vertical;
            var currentDirection = startingPlacement.Direction;

            int nextHorizontal = 0;
            int nextVertical = 0;

            bool isIllegal;

            //Check if starting placement is outside the table
            isIllegal = CheckForIllegalMove(currentHorizontal, currentVertical);

            if (isIllegal)
            {
                Message.PlacedOutsideTable(currentHorizontal, currentVertical);
                return;
            }

            foreach (var instruction in instructions)
            {
                if (instruction.Type == InstructionType.MOVE)
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

                    else if (currentDirection == Direction.ILLEGAL)
                    {
                        Message.IllegalDirection();
                        continue;
                    }

                    if (isIllegal)
                    {
                        Message.OutOfBounds(nextHorizontal, nextVertical);
                        continue;
                    }

                    else if (!isIllegal)
                    {
                        currentHorizontal = nextHorizontal;
                        currentVertical = nextVertical;
                        continue;
                    }
                }

                else if (instruction.Type == InstructionType.REPORT)
                {
                    Message.PrintReport(currentHorizontal, currentVertical, currentDirection);
                    continue;
                }

                else if (instruction.Type == InstructionType.LEFT || instruction.Type == InstructionType.RIGHT)
                {
                    currentDirection = Turn(instruction.Type, currentDirection);
                    continue;
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
