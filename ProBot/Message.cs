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

        public static void PrintReport(int vertical, int horizontal, Direction direction)
        {
            Console.WriteLine($"{vertical}, {horizontal}, {direction.ToString()}\n\nPress enter to exit");
            Console.ReadLine();
        }
    }
}