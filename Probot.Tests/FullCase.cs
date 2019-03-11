using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProBot.Tests
{
    public class FullCase
    {
        InstructionService instructionService;
        MovementService movementService;

        public FullCase()
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
            var rawInstructions = new List<string> { "PLACE 0,0,WEST", "MOVE", "RIGHT", "MOVE", "MOVE", "REPORT" };

            var parsedInstructions = instructionService.ParseRawInstructions(rawInstructions);

            var report = movementService.Move(parsedInstructions);

            Assert.Equal("0,2, NORTH", report);
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
    }
}
