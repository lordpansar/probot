namespace ProBot
{
    public class Instruction
    {
        public Instruction()
        {
            Type = new InstructionType();
            Placement = new Position();
            Direction = new Direction();
        }

        public InstructionType Type { get; set; }
        //public bool IsPlacement { get; set; }
        public Position Placement { get; set; }
        public Direction Direction { get; set; }
    }
}
