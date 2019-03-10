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
        public void AssertThatDirectionCanBeParsed(string input, Direction direction)
        {
            var returnValue = Program.ParseDirection(input);

            Assert.Equal(direction, returnValue);
        }

        [Fact]
        public void AssertThatFaultyDirectionIsSetToIllegal()
        {
            var direction = Program.ParseDirection("NORTHWEST");

            Assert.Equal(Direction.ILLEGAL, direction);
        }

        [Fact]
        public void AssertThatRawInstructionsGetsParsed()
        {
            var setup = new List<string> { "PLACE 0,1,EAST", "MOVE", "REPORT" };

            var correctlyParsedInstruction = new Instruction
            {
                Direction = Direction.EAST,
                InstructionsList = new List<string>(),
                StartPosition = new Position { Horizontal = 0, Vertical = 1 }
            };

            var returnValue = Program.ParseRawInstructions(setup);

            var areEqual = CompareInstructions(returnValue, correctlyParsedInstruction);

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
            if (cleanedInstruction.InstructionsList.Equals(correctlyCleanedInstruction.InstructionsList))
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
