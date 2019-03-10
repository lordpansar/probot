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

            var parsedInstructions = ParseRawInstructions(rawInstructions);

            return parsedInstructions;
        }

        public List<Instruction> ParseRawInstructions(List<string> rawInstructionsList)
        {
            var instructions = new List<Instruction>();

            foreach (var rawInstruction in rawInstructionsList)
            {
                var instruction = new Instruction();

                if (rawInstruction.Contains("PLACE"))
                {
                    var values = rawInstruction.Split(',');

                    instruction.Type = InstructionType.PLACE;
                    instruction.Position.Vertical = int.Parse(values[0].Substring(values[0].Length - 1));
                    instruction.Position.Horizontal = int.Parse(values[1]);
                    instruction.Direction = ParseDirection(values[2]);
                    
                    instructions.Add(instruction);
                    continue;
                }
                else if(rawInstruction == "MOVE")
                {
                    instruction.Type = InstructionType.MOVE;
                    instruction.Position.Vertical = instructions[0].Position.Vertical;
                    instruction.Position.Horizontal = instructions[0].Position.Horizontal;
                    instruction.Direction = instructions[0].Direction;

                    instructions.Add(instruction);
                    continue;
                }
                else if(rawInstruction == "LEFT")
                {
                    instruction.Type = InstructionType.LEFT;
                    instruction.Position.Vertical = instructions[0].Position.Vertical;
                    instruction.Position.Horizontal = instructions[0].Position.Horizontal;
                    instruction.Direction = instructions[0].Direction;
                    
                    instructions.Add(instruction);
                    continue;
                }
                else if (rawInstruction == "RIGHT")
                {
                    instruction.Type = InstructionType.RIGHT;
                    instruction.Position.Vertical = instructions[0].Position.Vertical;
                    instruction.Position.Horizontal = instructions[0].Position.Horizontal;
                    instruction.Direction = instructions[0].Direction;
                    
                    instructions.Add(instruction);
                    continue;
                }
                else if(rawInstruction == "REPORT")
                {
                    instruction.Type = InstructionType.REPORT;
                    instruction.Position.Vertical = instructions[0].Position.Vertical;
                    instruction.Position.Horizontal = instructions[0].Position.Horizontal;
                    instruction.Direction = instructions[0].Direction;

                    instructions.Add(instruction);
                    continue;
                }
                else
                {
                    continue;
                }
            }

            return instructions;
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
