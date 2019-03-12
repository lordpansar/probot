using System;
using System.Collections.Generic;

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
            Console.WriteLine($"{horizontal},{vertical} is outside the table, all instructions until next placement has been ignored. ProBot does not approve of your shenanigans.");
        }

        public static void NeverPlacedOnTable()
        {
            Console.WriteLine($"You never placed ProBot on the table. ProBot does not approve of your shenanigans.");
        }

        public static void IllegalDirection()
        {
            Console.WriteLine($"Illegal direction is against the law! ProBot does not approve of your shenanigans.");
        }

        public static string GetReport(int horizontal, int vertical, Direction direction)
        {
            string report = $"{horizontal},{vertical}, {direction.ToString()}";
            return report;
        }

        public static void PrintReport(int horizontal, int vertical, Direction direction)
        {
            string report = $"{horizontal},{vertical}, {direction.ToString()}";
            Console.WriteLine($"\nREPORT\n{report}");
        }

        public static void PrintPath(List<Position> positions)
        {
            var table = new char[5, 5];

            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    table[row, column] = 'x';
                }
            }

            foreach (var position in positions)
            {
                table[position.Vertical, position.Horizontal] = 'o';
            }

            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    if (table[row, column] == 'x')
                    {
                        Console.Write(table[row, column]);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(table[row, column]);
                        Console.ResetColor();
                    }
                }
                Console.Write("\n");
            }
        }
    }
}