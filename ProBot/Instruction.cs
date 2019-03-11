namespace ProBot
{
    public class Instruction
    {
        public Instruction()
        {
            Type = new InstructionType();
            StartPosition = new Position();
            Direction = new Direction();
        }

        public InstructionType Type { get; set; }
        public Position StartPosition { get; set; }
        public Direction Direction { get; set; }
    }
}
