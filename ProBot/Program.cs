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

        private void CheckForIllegalMove()
        {
            throw new NotImplementedException();
        }

        private static void GetInstructions()
        {
            string wanted_path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\Instructions.txt"));
            var streamReader = new StreamReader(wanted_path);

            var line = streamReader.ReadLine();

            streamReader.Dispose();
        }
    }
}
