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
            Moves = new List<string>();
        }

        public Position StartPosition { get; set; }
        public Direction Direction { get; set; }
        public List<string> Moves { get; set; }
    }
}
