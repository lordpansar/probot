namespace ProBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var movementService = new MovementService();
            var instructionService = new InstructionService();

            var instruction = instructionService.GetInstructions();

            movementService.Move(instruction);
        }
    }
}
