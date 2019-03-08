using System.Collections.Generic;
using Xunit;

namespace ProBot.Tests
{
    public class Tests
    {
        [Theory]
        [InlineData("NORTH", Direction.NORTH)]
        [InlineData("EAST", Direction.EAST)]
        [InlineData("SOUTH", Direction.SOUTH)]
        [InlineData("WEST", Direction.WEST)]
        public void AssertThatDirectionCanBeRetrieved(string input, Direction direction)
        {
            var returnValue = Program.GetDirection(input);

            Assert.Equal(direction, returnValue);
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

            var returnValue = Program.CleanRawInstructions(setup);

            var areEqual = CompareInstructions(returnValue, correctlyCleanedInstruction);

            Assert.True(areEqual);
        }

        [Theory]
        [InlineData("LEFT", Direction.NORTH, Direction.WEST)]
        [InlineData("LEFT", Direction.EAST, Direction.NORTH)]
        [InlineData("LEFT", Direction.SOUTH, Direction.EAST)]
        [InlineData("LEFT", Direction.WEST, Direction.SOUTH)]
        [InlineData("RIGHT", Direction.NORTH, Direction.EAST)]
        [InlineData("RIGHT", Direction.EAST, Direction.SOUTH)]
        [InlineData("RIGHT", Direction.SOUTH, Direction.WEST)]
        [InlineData("RIGHT", Direction.WEST, Direction.NORTH)]
        public void AssertThatRobotCanTurn(string turn, Direction currentDirection, Direction newDirection)
        {
            var returnValue = Program.Turn(turn, currentDirection);

            Assert.Equal(newDirection, returnValue);
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
