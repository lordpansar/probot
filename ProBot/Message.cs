using System;

namespace ProBot
{
    public static class Message
    {
        public static void OutOfBounds(int horizontal, int vertical)
        {
            Console.WriteLine($"The instructed move to {horizontal},{vertical} is illegal and has been ignored. ProBot does not approve of your shenanigans.\n");
        }

        public static void PlacedOutsideTable(int horizontal, int vertical)
        {
            Console.WriteLine($"{horizontal},{vertical} is outside the table, all instructions has been ignored. ProBot does not approve of your shenanigans.\n\nPress enter to exit");
            Console.ReadLine();
        }

        public static void IllegalDirection()
        {
            Console.WriteLine($"Illegal direction is against the law! ProBot does not approve of your shenanigans.\n\nPress enter to exit");
        }

        public static void PrintMove()
        {
            throw new NotImplementedException();
        }

        public static void PrintReport(int horizontal, int vertical, Direction direction)
        {
            Console.WriteLine($"REPORT\n{horizontal},{vertical}, {direction.ToString()}\n\nPress enter to exit");
            Console.ReadLine();
        }
    }
}