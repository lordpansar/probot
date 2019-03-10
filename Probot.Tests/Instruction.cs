using System.Collections.Generic;
using Xunit;

namespace ProBot.Tests
{
    public class Instruction
    {
        InstructionService instructionService;

        public Instruction()
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
            var instructionsList = new List<global::ProBot.Instruction>();

            var place = new global::ProBot.Instruction { Type = InstructionType.PLACE, Direction = Direction.EAST, Position = new Position { Vertical = 0, Horizontal = 1 } };
            var move = new global::ProBot.Instruction { Type = InstructionType.MOVE, Direction = Direction.EAST, Position = new Position { Vertical = 0, Horizontal = 1 } };
            var report = new global::ProBot.Instruction { Type = InstructionType.REPORT, Direction = Direction.EAST, Position = new Position { Vertical = 0, Horizontal = 1 } };

            instructionsList.Add(place);
            instructionsList.Add(move);
            instructionsList.Add(report);

            var returnValue = instructionService.ParseRawInstructions(setup);

            var areEqual = CompareInstructionLists(returnValue, instructionsList);

            Assert.True(areEqual);
        }

        private bool CompareInstructionLists(List<global::ProBot.Instruction> expected, List<global::ProBot.Instruction> actual)
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
                if (expected[i].Position.Vertical != actual[i].Position.Vertical || expected[i].Position.Horizontal != actual[i].Position.Horizontal)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
