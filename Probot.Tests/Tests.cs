using System.Collections.Generic;
using Xunit;

namespace ProBot.Tests
{
    public class Tests
    {
        MovementService movementService;
        InstructionService instructionService;

        public Tests()
        {
            movementService = new MovementService();
            instructionService = new InstructionService();
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
        public void AssertThatFaultyDirectionIsSetToIllegal()
        {
            var direction = instructionService.ParseDirection("NORTHWEST");

            Assert.Equal(Direction.ILLEGAL, direction);
        }

        [Theory]
        [InlineData(6,6)]
        [InlineData(-1, 5)]
        [InlineData(2, -1)]
        [InlineData(-1, -1)]
        public void AssertThatIllegalMoveIsDetected(int vertical, int horizontal)
        {
            var isIllegal = movementService.CheckForIllegalMove(vertical, horizontal);
            Assert.True(isIllegal);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 5)]
        public void AssertThatLegalMoveIsApproved(int vertical, int horizontal)
        {
            var isIllegal = movementService.CheckForIllegalMove(vertical, horizontal);
            Assert.False(isIllegal);
        }

        [Fact]
        public void AssertThatRawInstructionsGetsParsed()
        {
            var setup = new List<string> { "PLACE 0,1,EAST", "MOVE", "REPORT" };
            var instructionsList = new List<Instruction>();

            var place = new Instruction { Type = InstructionType.PLACE, Direction = Direction.EAST, Position = new Position { Vertical = 0, Horizontal = 1 } };
            var move = new Instruction { Type = InstructionType.MOVE, Direction = Direction.EAST, Position = new Position { Vertical = 0, Horizontal = 1 } };
            var report = new Instruction { Type = InstructionType.REPORT, Direction = Direction.EAST, Position = new Position { Vertical = 0, Horizontal = 1 } };

            instructionsList.Add(place);
            instructionsList.Add(move);
            instructionsList.Add(report);

            var returnValue = instructionService.ParseRawInstructions(setup);

            var areEqual = CompareInstructionLists(returnValue, instructionsList);

            Assert.True(areEqual);
        }

        [Theory]
        [InlineData(InstructionType.LEFT, Direction.NORTH, Direction.WEST)]
        [InlineData(InstructionType.LEFT, Direction.EAST, Direction.NORTH)]
        [InlineData(InstructionType.LEFT, Direction.SOUTH, Direction.EAST)]
        [InlineData(InstructionType.LEFT, Direction.WEST, Direction.SOUTH)]
        [InlineData(InstructionType.RIGHT, Direction.NORTH, Direction.EAST)]
        [InlineData(InstructionType.RIGHT, Direction.EAST, Direction.SOUTH)]
        [InlineData(InstructionType.RIGHT, Direction.SOUTH, Direction.WEST)]
        [InlineData(InstructionType.RIGHT, Direction.WEST, Direction.NORTH)]
        public void AssertThatRobotCanTurn(InstructionType turn, Direction currentDirection, Direction newDirection)
        {
            var returnValue = movementService.Turn(turn, currentDirection);

            Assert.Equal(newDirection, returnValue);
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
                if (expected[i].Position.Vertical != actual[i].Position.Vertical || expected[i].Position.Horizontal != actual[i].Position.Horizontal)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
