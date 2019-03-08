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
        {
            string wanted_path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Instructions.txt"));
            var streamReader = new StreamReader(wanted_path);

            var line = streamReader.ReadLine();

            streamReader.Dispose();
        }
    }
}
