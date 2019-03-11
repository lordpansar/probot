using System;
using System.Collections.Generic;
using System.IO;

namespace ProBot
{
    public class InstructionService
    {
        public Manifest GetManifest()
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Instructions.txt"));

            var lineArray = File.ReadAllLines(path);
            var rawInstructions = new List<string>(lineArray);

            var manifest = ParseRawInstructions(rawInstructions);

            return manifest;
        }

        public Manifest ParseRawInstructions(List<string> rawInstructionsList)
        {
            var manifest = new Manifest();

            foreach (var rawInstruction in rawInstructionsList)
            {
                var instruction = new Instruction();

                if (rawInstruction.Contains("PLACE"))
                {
                    var values = rawInstruction.Split(',');

                    instruction.Type = InstructionType.PLACE;
                    
                    manifest.StartPosition.Horizontal = int.Parse(values[1]);
                    manifest.StartPosition.Vertical = int.Parse(values[0].Substring(values[0].Length - 1));
                    manifest.StartDirection = ParseDirection(values[2]);
                    manifest.Instructions.Add(instruction);

                    continue;
                }
                else if(rawInstruction == "MOVE")
                {
                    instruction.Type = InstructionType.MOVE;
                }
                else if(rawInstruction == "LEFT")
                {
                    instruction.Type = InstructionType.LEFT;
                }
                else if (rawInstruction == "RIGHT")
                {
                    instruction.Type = InstructionType.RIGHT;
                }
                else
                {
                    instruction.Type = InstructionType.REPORT;
                }

                manifest.Instructions.Add(instruction);
            }

            return manifest;
        }

        public Direction ParseDirection(string input)
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
