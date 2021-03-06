using System;

namespace ProBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var movementService = new MovementService();
            var instructionService = new InstructionService();

            var instructions = instructionService.GetInstructions();

            var success = movementService.ExecuteInstructions(instructions);
            Console.ReadLine();
        }
    }
}
