using System;
using System.Collections.Generic;
using System.IO;

namespace ProBot
{
    public class Program
    {
        static void Main(string[] args)
        {
            var table = new int[5,5];
            var instruction = GetInstructions();

            Move(instruction);
        }

        public static void Move(Instruction instruction)
        {
            throw new NotImplementedException();
        public static bool CheckForIllegalMove(int vertical, int horizontal)
        {
            if(horizontal > 5 || horizontal < 0 || vertical > 5 || vertical < 0)
            {
                return true;
            }
            else
            {
                Message.OutOfBounds();
                return false;
            }
        }

        public static Direction Turn(string turn, Direction currentDirection)
        {
            Direction newDirection = new Direction();

            if(turn == "LEFT")
            {
                if (currentDirection == Direction.NORTH)
                {
                    newDirection = Direction.WEST;
                }
                if (currentDirection == Direction.EAST)
                {
                    newDirection = Direction.NORTH;
                }
                if (currentDirection == Direction.SOUTH)
                {
                    newDirection = Direction.EAST;
                }
                if (currentDirection == Direction.WEST)
                {
                    newDirection = Direction.SOUTH;
                }
            }
            else
            {
                if (currentDirection == Direction.NORTH)
                {
                    newDirection = Direction.EAST;
                }
                if (currentDirection == Direction.EAST)
                {
                    newDirection = Direction.SOUTH;
                }
                if (currentDirection == Direction.SOUTH)
                {
                    newDirection = Direction.WEST;
                }
                if (currentDirection == Direction.WEST)
                {
                    newDirection = Direction.NORTH;
                }
            }
            
            return newDirection;
        }

        public static Instruction GetInstructions()
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Instructions.txt"));

            var lineArray = File.ReadAllLines(path);
            var rawInstructions = new List<string>(lineArray);

            var cleanedInstructions = CleanRawInstructions(rawInstructions);

            return cleanedInstructions;
        }
        public static Direction GetDirection(string input)
        {
            Direction direction = new Direction();

            switch (input)
            {
                case "NORTH":
                    direction = Direction.NORTH;
                    break;
                case "EAST":
                    direction = Direction.EAST;
                    break;
                case "SOUTH":
                    direction = Direction.SOUTH;
                    break;
                case "WEST":
                    direction = Direction.WEST;
                    break;
                default:
                    direction = Direction.ILLEGAL;
                    break;
            }

            return direction;
        }
    }
}
