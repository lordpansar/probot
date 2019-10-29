# ProBot
Welcome to ProBot!

ProBot is a simulation of a tiny robot navigating a table. The table consists of 5x5 tiles (0-4 on the X and Y axis). 
0,0 being the coordinates for the bottom left corner, and 4,4 the top right corner.

# Providing instructions

In solution, open Instructions.txt and enter the instructions for the robot, one instruction per row.

The following types of instructions are accepted:

PLACE n,n,[Direction] 

The PLACE command sets the inital placement of the robot.

the n variables represents the starting coordinates should be replaced by integers. The first integer represents the X axis and the second one the Y axis. Valid coordinates are within the span of 0-4. 

Direction states in which direction the robot is facing. Valid directions are NORTH, SOUTH, WEST, EAST.

Be sure to get the commas in the correct order.

MOVE

Moves the robot one tile in the direction that it is currently facing. A move that would the make robot fall off the table will be ignored.

RIGHT

Turns the robot 90 degrees to the right in it's current position. E g: robot is facing north. A single RIGHT command will make it face east.

LEFT

Turns the robot 90 degrees to the left in it's current position. E g: robot is facing north. A single LEFT command will make it face west.

REPORT

Prints the robots current position (coordinates and facing direction) to the console. 



Whitespace such as tabs and newlines will be ignored by the instruction parser.
Lines starting with // are comments and will be ignored by the instruction parser as well.

Remember to save Instructions.txt after making changes, otherwise they will not take place.

Run the project to try out your instructions.

# Example instructions

//VALID INSTRUCTIONS

PLACE 0,0,NORTH //Place robot on the tile on X axis 0 and Y axis 0

MOVE //Move robot 1 tile north

RIGHT //Make robot face east

MOVE //Move robot 1 tile east

MOVE //Move robot 1 tile east

REPORT //Print robot's current position and direction to console

-----------------------------------

//INVALID INSTRUCTIONS EXAMPLE

PLACE 5,5,NORTH

MOVE

MOVE

REPORT

//Robot is placed outside the table. All instructions following the place command will be ignored.


# Test data

If you're not feeling creative at the moment there is some prepared test data in the solution, in file TestData.txt.
Copy+paste the instructions you want to run inside Instructions.txt and save changes before running the application.

These use cases are also represented as automated tests in ProBot.Tests/UseCase.cs.

# Run me!

Run the application, e.g by pressing F5 in Visual Studio for Windows.

If you provided correctly formatted and legal instructions, the app will print a map of the route travelled by the robot. The 's' on the map marks the starting point and 'f' marks the final position.
