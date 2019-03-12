using System.Collections.Generic;
using Xunit;

namespace ProBot.Tests
{
    public class MovementTest
    {
        MovementService movementService;

        public MovementTest()
        {
            movementService = new MovementService();
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(-1, 4)]
        [InlineData(2, -1)]
        [InlineData(-1, -1)]
        public void AssertThatIllegalMoveIsDetected(int horizontal, int vertical)
        {
            var isIllegal = movementService.CheckForIllegalMove(horizontal, vertical);
            Assert.True(isIllegal);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(4, 4)]
        [InlineData(0, 4)]
        [InlineData(4, 0)]
        public void AssertThatLegalMoveIsApproved(int horizontal, int vertical)
        {
            var isIllegal = movementService.CheckForIllegalMove(horizontal, vertical);
            Assert.False(isIllegal);
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

        [Theory]
        [InlineData(InstructionType.LEFT, Direction.ILLEGAL, Direction.ILLEGAL)]
        [InlineData(InstructionType.RIGHT, Direction.ILLEGAL, Direction.ILLEGAL)]
        public void AssertThatRobotCanNotTurnIllegaly(InstructionType turn, Direction currentDirection, Direction newDirection)
        {
            var returnValue = movementService.Turn(turn, currentDirection);

            Assert.Equal(newDirection, returnValue);
        }

        [Fact]
        public void AssertThatEmptyInstructionsListIsNotRun()
        {
            var list = new List<Instruction>();

            var isExecuted = movementService.ExecuteInstructions(list);

            Assert.False(isExecuted);
        }

        [Fact]
        public void AssertThatInstructionsListIsRun()
        {
            var list = new List<Instruction>();
            var instruction = new Instruction { Type = InstructionType.PLACE, Direction = Direction.NORTH };
            list.Add(instruction);

            var isExecuted = movementService.ExecuteInstructions(list);

            Assert.True(isExecuted);
        }

        [Theory]
        [InlineData(0, 4)]
        [InlineData(1, 3)]
        [InlineData(2, 2)]
        [InlineData(3, 1)]
        [InlineData(4, 0)]
        public void AssertThatPositionsAreParsedCorrectly(int vertical, int expected)
        {
            var positions = new List<Position>();
            var position = new Position { Horizontal = 0, Vertical = vertical };
            positions.Add(position);

            var parsedPositions = movementService.TranslateVerticalCoordinates(positions);

            Assert.Equal(expected, parsedPositions[0].Vertical);
        }
    }
}
