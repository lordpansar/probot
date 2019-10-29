# ProBot
Welcome to ProBot!

ProBot is a simulation of a tiny robot navigating a table. The table consists of 5x5 tiles (0-4 on the X and Y axis).

# Providing instructions

In solution, open Instructions.txt and enter the instructions for the robot, one instruction per row. Run the application.

The following types of instructions are accepted:

PLACE n,n,[Direction] 

The PLACE command and N variables sets the inital placement of the robot. 
0,0 being the coordinates for the bottom left corner. Direction states in which direction the robot is facing. Valid directions are NORTH, SOUTH, WEST, EAST.

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

# Test data

If you're not feeling creative at the moment there is some prepared test data in the solution, in file TestData.txt.
Copy+paste the instructions you want to run inside Instructions.txt and save changes before running the application.

These use cases are also represented as automated tests in ProBot.Tests/UseCase.cs.

# Run me!

Run the application. If you provided correctly formatted and legal instructions, the app will print a map of the route travelled by the robot. The 's' on the map marks the starting point and 'f' marks the final position.
