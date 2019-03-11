namespace ProBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var movementService = new MovementService();
            var instructionService = new InstructionService();

            var manifest = instructionService.GetManifest();

            movementService.Move(manifest);
        }
    }
}
