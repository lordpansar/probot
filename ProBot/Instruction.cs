namespace ProBot
{
    public class Instruction
    {
        public Instruction()
        {
            Type = new InstructionType();
            Position = new Position();
            Direction = new Direction();
        }

        public InstructionType Type { get; set; }
        public Position Position { get; set; }
        public Direction Direction { get; set; }
    }
}
