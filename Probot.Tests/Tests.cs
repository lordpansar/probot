using ProBot;
using Xunit;

namespace Probot.Tests
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
    }
}
