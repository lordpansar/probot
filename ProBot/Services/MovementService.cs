﻿using System.Collections.Generic;
using System.Linq;

namespace ProBot
{
    public class MovementService
    {
        public bool ExecuteInstructions(List<Instruction> instructions)
        {
            bool executed;

            if (instructions.Count == 0)
            {
                Message.NeverPlacedOnTable();
                executed = false;
            }
            else
            {
                ExecuteRoute(instructions);
                executed = true;
            }

            return executed;
        }

        public string ExecuteRoute(List<Instruction> instructions)
        {
            var currentHorizontal = 0;
            var currentVertical = 0;
            var currentDirection = new Direction();

            var nextHorizontal = 0;
            var nextVertical = 0;

            var numberOfReports = instructions.Where(x => x.Type == InstructionType.REPORT).Count();
            var isIllegal = false;
            var isOnTable = false;

            var positionLog = new List<Position>();

            foreach (var instruction in instructions)
            {
                if(instruction.Type == InstructionType.PLACE)
                {
                    //Check if starting placement is outside the table
                    isIllegal = CheckForIllegalMove(instruction.LastPlacement.Horizontal, instruction.LastPlacement.Vertical);

                    if (isIllegal)
                    {
                        isOnTable = false;
                        Message.PlacedOutsideTable(instruction.LastPlacement.Horizontal, instruction.LastPlacement.Vertical);
                    }
                    else
                    {
                        isOnTable = true;
                        currentHorizontal = instruction.LastPlacement.Horizontal;
                        currentVertical = instruction.LastPlacement.Vertical;
                        currentDirection = instruction.Direction;

                        var pos = new Position { Horizontal = currentHorizontal, Vertical = currentVertical };
                        positionLog.Add(pos);

                        //If instructions list consists of only one place instruction
                        if (instructions.Count == 1)
                        {
                            return Message.GetReport(currentHorizontal, currentVertical, currentDirection);
                        }
                    }
                }

                else if (isOnTable && instruction.Type == InstructionType.MOVE)
                {
                    var moveReturnValues = Move(currentDirection, currentHorizontal, currentVertical);

                    if (moveReturnValues.isIllegal)
                    {
                        Message.OutOfBounds(moveReturnValues.nextHorizontal, moveReturnValues.nextVertical);
                    }
                    else
                    {
                        currentHorizontal = moveReturnValues.nextHorizontal;
                        currentVertical = moveReturnValues.nextVertical;

                        var pos = new Position { Horizontal = currentHorizontal, Vertical = currentVertical };
                        positionLog.Add(pos);
                    }
                }

                else if (isOnTable && instruction.Type == InstructionType.LEFT || isOnTable && instruction.Type == InstructionType.RIGHT)
                {
                    currentDirection = Turn(instruction.Type, currentDirection);
                }

                else if(isOnTable && instruction.Type == InstructionType.REPORT)
                {
                    //Check if instructions list contains more than 1 report
                    if (numberOfReports > 1)
                    {
                        Message.PrintReport(currentHorizontal, currentVertical, currentDirection);
                        numberOfReports--;
                    }
                    else
                    {
                        var translatedPositions = ParseVerticalCoordinates(positionLog);

                        Message.PrintPath(translatedPositions);
                        Message.PrintReport(currentHorizontal, currentVertical, currentDirection);
                        return Message.GetReport(currentHorizontal, currentVertical, currentDirection);
                    }
                }
            }

            return string.Empty;
        }

        public (int nextHorizontal, int nextVertical, bool isIllegal) Move(Direction currentDirection, int currentHorizontal, int currentVertical)
        {
            int nextHorizontal = 0;
            int nextVertical = 0;
            bool isIllegal = false;

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
            }

            return (nextHorizontal, nextVertical, isIllegal);
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

        public List<Position> ParseVerticalCoordinates(List<Position> positions)
        { 
            var parsedPositions = new List<Position>();

            foreach (var position in positions)
            {
                if (position.Vertical == 0)
                {
                    position.Vertical = 4;
                }
                else if (position.Vertical == 1)
                {
                    position.Vertical = 3;
                }
                else if (position.Vertical == 2)
                {
                    position.Vertical = 2;
                }
                else if (position.Vertical == 3)
                {
                    position.Vertical = 1;
                }
                else if (position.Vertical == 4)
                {
                    position.Vertical = 0;
                }

                parsedPositions.Add(position);
            }

            return parsedPositions;
        }
    }
}