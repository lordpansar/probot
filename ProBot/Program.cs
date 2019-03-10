using System;
using System.Collections.Generic;
using System.IO;

namespace ProBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var table = new int[5,5];
            var instruction = GetInstructions();

            Move(instruction);
        }

        public static void Move(Instruction instructions)
        {
            var currentVertical = instructions.StartPosition.Vertical;
            var currentHorizontal = instructions.StartPosition.Horizontal;
            var currentDirection = instructions.Direction;

            int nextVertical = 0;
            int nextHorizontal = 0;

            bool isIllegal;

            //Check if placement is outside the table
            isIllegal = CheckForIllegalMove(currentVertical, currentHorizontal);

            if(isIllegal)
            {
                return;
            }

            foreach (var instruction in instructions.InstructionsList)
            {
                 if(instruction == "MOVE")       
                 {       
                     if(currentDirection == Direction.NORTH)       
                     {       
                        nextVertical = currentVertical - 1;       
                        nextHorizontal = currentHorizontal;       

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);       
                     }       
                     if (currentDirection == Direction.EAST)       
                     {       
                        nextVertical = currentVertical;       
                        nextHorizontal = currentHorizontal + 1;       

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);       
                     }       
                     if (currentDirection == Direction.SOUTH)       
                     {       
                        nextVertical = currentVertical + 1;       
                        nextHorizontal = currentHorizontal;       

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);       
                     }       
                     if (currentDirection == Direction.WEST)       
                     {       
                        nextVertical = currentVertical;       
                        nextHorizontal = currentHorizontal - 1;       

                        isIllegal = CheckForIllegalMove(nextVertical, nextHorizontal);       
                     }       

                     if(isIllegal)
                     {
                        continue;
                     }

                     if(!isIllegal)       
                     {       
                        currentVertical = nextVertical;       
                        currentHorizontal = nextHorizontal;       
                        continue;       
                     }
                 }       

                 if(instruction == "REPORT")       
                 {       
                     Message.PrintReport(currentHorizontal, currentVertical, currentDirection);       
                 }       

                 if(instruction == "LEFT" || instruction == "RIGHT")       
                 {       
                     currentDirection = Turn(instruction, currentDirection);       
                     continue;       
                 }       
            }
        }

        public static bool CheckForIllegalMove(int vertical, int horizontal)
        {
            if(horizontal > 5 || horizontal < 0 || vertical > 5 || vertical < 0)
            {
                Message.OutOfBounds();
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Direction Turn(string turn, Direction currentDirection)
        {
            Direction newDirection = new Direction();

            if(turn == "LEFT")
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

        public static Instruction GetInstructions()
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Instructions.txt"));

            var lineArray = File.ReadAllLines(path);
            var rawInstructions = new List<string>(lineArray);

            var cleanedInstructions = ParseRawInstructions(rawInstructions);

            return cleanedInstructions;
        }

        public static Instruction ParseRawInstructions(List<string> rawInstructionsList)
        {
            //TODO: Skriv klart denna metod så den hanterar alla typer av instruktioner
            var instruction = new Instruction();
            var instructionsList = new List<string>();

            foreach (var rawInstruction in rawInstructionsList)
            {
                if(rawInstruction.Contains("PLACE"))
                {
                    var values = rawInstruction.Split(',');
                    
                    instruction.StartPosition.Horizontal = int.Parse(values[0].Substring(values[0].Length - 1));
                    instruction.StartPosition.Vertical = int.Parse(values[1]);
                    instruction.Direction = ParseDirection(values[2]);
                    
                    continue;
                }

                else
                {
                    instructionsList.Add(rawInstruction);
                    continue;
                }
            }

            instruction.InstructionsList = instructionsList;

            return instruction;
        }

        public static Direction ParseDirection(string input)
        {
            Direction direction = new Direction();

            switch (input)
            {
                case "NORTH":
                    direction = Direction.NORTH;
                    break;
                case "EAST":
                    direction = Direction.EAST;
                    break;
                case "SOUTH":
                    direction = Direction.SOUTH;
                    break;
                case "WEST":
                    direction = Direction.WEST;
                    break;
                default:
                    direction = Direction.ILLEGAL;
                    break;
            }

            return direction;
        }
    }
}
