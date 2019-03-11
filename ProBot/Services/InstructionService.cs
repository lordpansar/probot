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

                if (rawInstruction.Contains("PLACE"))
                {
                    var values = rawInstruction.Split(',');

                    instruction.Type = InstructionType.PLACE;
                    //instruction.IsPlacement = true;

                    instruction.Placement.Horizontal = int.Parse(values[1]);
                    instruction.Placement.Vertical = int.Parse(values[0].Substring(values[0].Length - 1));
                    instruction.Direction = ParseDirection(values[2]);

                    parsedInstructions.Add(instruction);

                    instructionsListHasPlace = true;

                    continue;
                }
                else if(instructionsListHasPlace && rawInstruction == "MOVE")
                {
                    instruction.Type = InstructionType.MOVE;
                }
                else if(instructionsListHasPlace && rawInstruction == "LEFT")
                {
                    instruction.Type = InstructionType.LEFT;
                }
                else if (instructionsListHasPlace && rawInstruction == "RIGHT")
                {
                    instruction.Type = InstructionType.RIGHT;
                }
                else if (instructionsListHasPlace && rawInstruction == "REPORT")
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
