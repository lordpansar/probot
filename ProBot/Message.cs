using System;

namespace ProBot
{
    public static class Message
    {
        public static void OutOfBounds()
        {
            Console.WriteLine("The instructed move is illegal. ProBot does not approve of your shenanigans.");
        }

        public static void PrintMove()
        {
            throw new NotImplementedException();
        }

        public static void PrintReport(int horizontal, int vertical, Direction direction)
        {
            Console.WriteLine($"{horizontal}, {vertical}, {direction.ToString()}\n\nPress enter to exit");
            Console.ReadLine();
        }
    }
}