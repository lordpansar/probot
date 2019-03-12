using System.Collections.Generic;
using Xunit;

namespace ProBot.Tests
{
    public class UseCase
    {
        InstructionService instructionService;
        MovementService movementService;

        public UseCase()
        {
            instructionService = new InstructionService();
            movementService = new MovementService();
        }

        [Fact]
        public void AssertThatStandardMovementsWithinTableAreOK()
        {
            var rawInstructions = new List<string> { "PLACE 0,0,NORTH", "MOVE", "RIGHT", "MOVE", "REPORT" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("1,1, EAST", report);
        }

        [Fact]
        public void AssertThatProBotCanNotWalkOffTable()
        {
            var rawInstructions = new List<string> { "PLACE 0,2,WEST", "MOVE", "LEFT", "MOVE", "MOVE", "REPORT" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("0,0, SOUTH", report);
        }

        [Fact]
        public void AssertThatProBotCanNotBePlacedOutsideTable()
        {
            var rawInstructions = new List<string> { "PLACE 5,5,NORTH", "MOVE", "MOVE", "REPORT" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("", report);
        }

        [Fact]
        public void AssertThatNoInputDoesNotCrashApplication()
        {
            var rawInstructions = new List<string>();

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("", report);
        }

        [Fact]
        public void AssertThatNoPlaceCommandIgnoresInstructions()
        {
            var rawInstructions = new List<string>{ "MOVE", "MOVE", "RIGHT", "REPORT" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("", report);
        }

        [Fact]
        public void AssertThatInstructionsBeforePlaceCommandIsIgnoredButNotAfter()
        {
            var rawInstructions = new List<string> { "MOVE", "PLACE 0,0,NORTH", "MOVE", "RIGHT", "REPORT" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("0,1, EAST", report);
        }

        [Fact]
        public void AssertThatSinglePlacementInstructionIsOK()
        {
            var rawInstructions = new List<string> { "PLACE 0,0,NORTH" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("0,0, NORTH", report);
        }

        [Fact]
        public void AssertThatSingleReportInstructionIsOK()
        {
            var rawInstructions = new List<string> { "REPORT" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("", report);
        }

        [Fact]
        public void AssertThatOnlyMultipleReportsAreOK()
        {
            var rawInstructions = new List<string> { "REPORT", "REPORT", "REPORT" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("", report);
        }

        [Fact]
        public void AssertThatMultipleReportsTogetherWithOtherInstructionsAreOK()
        {
            var rawInstructions = new List<string> { "PLACE 0,0,NORTH", "MOVE", "REPORT", "PLACE 0,1,EAST", "MOVE", "REPORT" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("1,1, EAST", report);
        }
    }
}
