using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProBot.Tests
{
    public class Movement
    {
        MovementService movementService;

        public Movement()
        {
            movementService = new MovementService();
        }

        

        [Theory]
        [InlineData(6, 6)]
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
    }
}
