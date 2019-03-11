using System.Collections.Generic;

namespace ProBot
{
    public class Manifest
    {
        public Manifest()
        {
            StartPosition = new Position();
            StartDirection = new Direction();
            Instructions = new List<Instruction>();
        }

        public Position StartPosition { get; set; }
        public Direction StartDirection { get; set; }
        public List<Instruction> Instructions { get; set; }
    }
}
