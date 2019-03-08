using System;

namespace ProBot
{
    public static class Error
    {
        public static void OutOfBounds()
        {
            Console.WriteLine("The instructed move is illegal. ProBot does not approve of your shenanigans.");
        }
    }
}