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

        private void Move()
        {
            throw new NotImplementedException();
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
