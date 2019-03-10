using System;

namespace ProBot
{
    public static class Message
    {
        public static void OutOfBounds(int vertical, int horizontal)
        {
            Console.WriteLine($"The instructed move to {vertical},{horizontal} is illegal and has been ignored. ProBot does not approve of your shenanigans.\n");
        }

        public static void PlacedOutsideTable(int vertical, int horizontal)
        {
            Console.WriteLine($"{vertical},{horizontal} is outside the table, all instructions has been ignored. ProBot does not approve of your shenanigans.\n\nPress enter to exit");
            Console.ReadLine();
        }

        public static void PrintMove()
        {
            throw new NotImplementedException();
        }

        public static void PrintReport(int vertical, int horizontal, Direction direction)
        {
            Console.WriteLine($"REPORT\n{vertical},{horizontal}, {direction.ToString()}\n\nPress enter to exit");
            Console.ReadLine();
        }
    }
}