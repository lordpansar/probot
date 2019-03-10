using System;
using System.Collections.Generic;
using System.Text;

namespace ProBot
{
    public class Instruction
    {
        public Instruction()
        {
            StartPosition = new Position();
            Direction = new Direction();
            InstructionsList = new List<string>();
        }

        public Position StartPosition { get; set; }
        public Direction Direction { get; set; }
        public List<string> InstructionsList { get; set; }
    }
}
