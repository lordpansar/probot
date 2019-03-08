using System.Collections.Generic;
using Xunit;

namespace ProBot.Tests
{
    public class Tests
    {
        [Fact]
        public void AssertThatDirectionCanBeRetrieved()
        {
            var direction = Program.GetDirection("EAST");

            Assert.Equal(Direction.EAST, direction);
        }

        [Fact]
        public void AssertThatFaultyDirectionIsSetToIllegal()
        {
            var direction = Program.GetDirection("NORTHWEST");

            Assert.Equal(Direction.ILLEGAL, direction);
        }

        [Fact]
        public void AssertThatRawInstructionsGetCleaned()
        {
            var setup = new List<string> { "PLACE 0,1,EAST", "MOVE", "REPORT" };

            var correctlyCleanedInstruction = new Instruction
            {
                Direction = Direction.EAST,
                Moves = new List<string>(),
                StartPosition = new Position { Horizontal = 0, Vertical = 1 }
            };

            var cleanedInstructions = Program.CleanRawInstructions(setup);

            var areEqual = CompareInstructions(cleanedInstructions, correctlyCleanedInstruction);

            Assert.True(areEqual);
        }

        private bool CompareInstructions(Instruction cleanedInstruction, Instruction correctlyCleanedInstruction)
        {
            if(cleanedInstruction.Direction != correctlyCleanedInstruction.Direction)
                return false;
            if (cleanedInstruction.Moves.Equals(correctlyCleanedInstruction.Moves))
                return false;
            if (cleanedInstruction.StartPosition.Horizontal != correctlyCleanedInstruction.StartPosition.Horizontal)
                return false;
            if (cleanedInstruction.StartPosition.Vertical != correctlyCleanedInstruction.StartPosition.Vertical)
                return false;
            else
                return true;
        }
    }
}
