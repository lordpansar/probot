using System.Collections.Generic;
using Xunit;

namespace ProBot.Tests
{
    public class InstructionTest
    {
        InstructionService instructionService;

        public InstructionTest()
        {
            instructionService = new InstructionService();
        }

        [Fact]
        public void AssertThatFaultyDirectionIsSetToIllegal()
        {
            var direction = instructionService.ParseDirection("NORTHWEST");
            
            Assert.Equal(Direction.ILLEGAL, direction);
        }

        [Theory]
        [InlineData("NORTH", Direction.NORTH)]
        [InlineData("EAST", Direction.EAST)]
        [InlineData("SOUTH", Direction.SOUTH)]
        [InlineData("WEST", Direction.WEST)]
        public void AssertThatDirectionCanBeParsed(string input, Direction direction)
        {
            var returnValue = instructionService.ParseDirection(input);

            Assert.Equal(direction, returnValue);
        }

        [Fact]
        public void AssertThatRawInstructionsGetsParsed()
        {
            var setup = new List<string> { "PLACE 0,1,EAST", "MOVE", "LEFT", "RIGHT", "REPORT" };
            var instructionsList = new List<Instruction>();

            var place = new Instruction { Type = InstructionType.PLACE };
            var move = new Instruction { Type = InstructionType.MOVE };
            var left = new Instruction { Type = InstructionType.LEFT };
            var right = new Instruction { Type = InstructionType.RIGHT };
            var report = new Instruction { Type = InstructionType.REPORT };

            instructionsList.Add(place);
            instructionsList.Add(move);
            instructionsList.Add(left);
            instructionsList.Add(right);
            instructionsList.Add(report);

            var returnValue = instructionService.ParseRawInstructions(setup);

            var areEqual = CompareInstructionLists(returnValue, instructionsList);

            Assert.True(areEqual);
        }

        [Fact]
        public void AssertThatWhiteSpaceInInputGetsParsedCorrectly()
        {
            var setup = new List<string> { "", "\t", "\n", " " };

            var returnValue = instructionService.ParseRawInstructions(setup);

            Assert.Empty(returnValue);
        }

        [Fact]
        public void AssertThatCommentsInInputGetsParsedCorrectly()
        {
            var setup = new List<string> { "//Test", "//This is a test", "//This is SERIOUSLY a test" };

            var returnValue = instructionService.ParseRawInstructions(setup);

            Assert.Empty(returnValue);
        }

        [Fact]
        public void AssertThatInvalidInputIsParsedCorrectly()
        {
            var setup = new List<string> { "All your base are belong to us", "PLAKE", "RIGHTEOUS" };

            var returnValue = instructionService.ParseRawInstructions(setup);

            Assert.Empty(returnValue);
        }

        private bool CompareInstructionLists(List<Instruction> expected, List<Instruction> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }
            
            for (int i = 0; i < actual.Count; i++)
            {
                if(expected[i].Type != actual[i].Type)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
