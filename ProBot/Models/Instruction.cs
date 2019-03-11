namespace ProBot
{
    public class Instruction
    {
        public Instruction()
        {
            Type = new InstructionType();
            LastPlacement = new Position();
            Direction = new Direction();
        }

        public InstructionType Type { get; set; }
        public Position LastPlacement { get; set; }
        public Direction Direction { get; set; }
    }
}
