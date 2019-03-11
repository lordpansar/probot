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
            var setup = new List<string> { "PLACE 0,1,EAST", "MOVE", "REPORT" };
            var instructionsList = new List<Instruction>();

            var place = new Instruction { Type = InstructionType.PLACE, Direction = Direction.EAST, StartPosition = new Position { Vertical = 0, Horizontal = 1 } };
            var move = new Instruction { Type = InstructionType.MOVE, Direction = Direction.EAST, StartPosition = new Position { Vertical = 0, Horizontal = 1 } };
            var report = new Instruction { Type = InstructionType.REPORT, Direction = Direction.EAST, StartPosition = new Position { Vertical = 0, Horizontal = 1 } };

            instructionsList.Add(place);
            instructionsList.Add(move);
            instructionsList.Add(report);

            var returnValue = instructionService.ParseRawInstructions(setup);

            var areEqual = CompareInstructionLists(returnValue, instructionsList);

            Assert.True(areEqual);
        }

        private bool CompareInstructionLists(List<Instruction> expected, List<Instruction> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }
            
            for (int i = 0; i < actual.Count; i++)
            {
                if(expected[i].Direction != actual[i].Direction)
                {
                    return false;
                }
                if (expected[i].Type != actual[i].Type)
                {
                    return false;
                }
                if (expected[i].StartPosition.Vertical != actual[i].StartPosition.Vertical || expected[i].StartPosition.Horizontal != actual[i].StartPosition.Horizontal)
                {
                    return false;
                }
            }

            return true;
        }
    }
}