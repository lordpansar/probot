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
            int startVertical = 0;
            int startHorizontal = 0;
            var direction = new Direction();

            foreach (var rawInstruction in rawInstructionsList)
            {
                var instruction = new Instruction();

                if (rawInstruction.Contains("PLACE"))
                {
                    var values = rawInstruction.Split(',');

                    instruction.Type = InstructionType.PLACE;
                    instruction.StartPosition.Vertical = int.Parse(values[0].Substring(values[0].Length - 1));
                    instruction.StartPosition.Horizontal = int.Parse(values[1]);
                    instruction.Direction = ParseDirection(values[2]);
                    
                    instructions.Add(instruction);

                    startVertical = instruction.StartPosition.Vertical;
                    startHorizontal = instruction.StartPosition.Horizontal;
                    direction = instruction.Direction;

                    continue;
                }
                else if(rawInstruction == "MOVE")
                {
                    instruction = AssembleInstruction(instruction, InstructionType.MOVE, startVertical, startHorizontal, direction);

                    instructions.Add(instruction);
                    continue;
                }
                else if(rawInstruction == "LEFT")
                {
                    instruction = AssembleInstruction(instruction, InstructionType.LEFT, startVertical, startHorizontal, direction);

                    instructions.Add(instruction);
                    continue;
                }
                else if (rawInstruction == "RIGHT")
                {
                    instruction = AssembleInstruction(instruction, InstructionType.RIGHT, startVertical, startHorizontal, direction);

                    instructions.Add(instruction);
                    continue;
                }
                else if(rawInstruction == "REPORT")
                {
                    instruction = AssembleInstruction(instruction, InstructionType.REPORT, startVertical, startHorizontal, direction);

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

        public Instruction AssembleInstruction(Instruction instruction, InstructionType type, int startVertical, int startHorizontal, Direction direction)
        {
            instruction.Type = type;
            instruction.StartPosition.Vertical = startVertical;
            instruction.StartPosition.Horizontal = startHorizontal;
            instruction.Direction = direction;

            return instruction;
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
