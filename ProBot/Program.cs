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

            var movementService = new MovementService();

            movementService.Move(instruction);
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
                    
                    instruction.StartPosition.Vertical = int.Parse(values[0].Substring(values[0].Length - 1));
                    instruction.StartPosition.Horizontal = int.Parse(values[1]);
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
