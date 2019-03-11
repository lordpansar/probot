using System;
using System.Collections.Generic;
using System.IO;

namespace ProBot
{
    public class InstructionService
    {
        public List<Instruction> GetInstructions()
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Instructions.txt"));

            var lineArray = File.ReadAllLines(path);
            var rawInstructions = new List<string>(lineArray);

            var instructions = ParseRawInstructions(rawInstructions);

            return instructions;
        }

        public List<Instruction> ParseRawInstructions(List<string> rawInstructionsList)
        {
            var parsedInstructions = new List<Instruction>();
            bool instructionsListHasPlace = false;

            foreach (var rawInstruction in rawInstructionsList)
            {
                var instruction = new Instruction();
                var cleanedFromWhitespace = rawInstruction.Replace(" ", "");

                //Check for whitespace or comments in instructions
                if (string.IsNullOrWhiteSpace(rawInstruction) || rawInstruction.Substring(0, 2) == "//")
                {
                    continue;
                }
                else if (rawInstruction.ToLower().Contains("place"))
                {
                    var values = cleanedFromWhitespace.Split(',');

                    instruction.Type = InstructionType.PLACE;

                    instruction.LastPlacement.Horizontal = int.Parse(values[0].Substring(values[0].Length - 1));
                    instruction.LastPlacement.Vertical = int.Parse(values[1]);
                    instruction.Direction = ParseDirection(values[2]);

                    parsedInstructions.Add(instruction);

                    instructionsListHasPlace = true;

                    continue;
                }
                else if(instructionsListHasPlace && rawInstruction.ToLower() == "move")
                {
                    instruction.Type = InstructionType.MOVE;
                }
                else if(instructionsListHasPlace && rawInstruction.ToLower() == "left")
                {
                    instruction.Type = InstructionType.LEFT;
                }
                else if (instructionsListHasPlace && rawInstruction.ToLower() == "right")
                {
                    instruction.Type = InstructionType.RIGHT;
                }
                else if (instructionsListHasPlace && rawInstruction.ToLower() == "report")
                {
                    instruction.Type = InstructionType.REPORT;
                }
                else
                {
                    continue;
                }

                parsedInstructions.Add(instruction);
            }

            return parsedInstructions;
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
