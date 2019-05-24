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
            //Generate table
            var table = GenerateTable();

            //Place tiles where ProBot has been placed, moved or finished the last instruction
            for (int position = 0; position < positions.Count; position++)
            {
                if(position == 0)
                {   //S = Start
                    table[positions[position].Vertical, positions[position].Horizontal] = 's';
                }
                else if (position == positions.Count -1)
                {   //F = Finish
                    table[positions[position].Vertical, positions[position].Horizontal] = 'f';
                }
                else
                {   //O = Not x
                    table[positions[position].Vertical, positions[position].Horizontal] = 'o';
                }
            }

            //Color & print tiles
            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    if (table[row, column] == 'x')
                    {
                        Console.Write(table[row, column]);
                    }
                    else if(table[row, column] == 'o')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(table[row, column]);
                        Console.ResetColor();
                    }
                    else if (table[row, column] == 's')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(table[row, column]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(table[row, column]);
                        Console.ResetColor();
                    }
                }
                Console.Write("\n");
            }
        }

        private static char[,] GenerateTable()
        {
            var table = new char[5, 5];

            //Generate table
            for (int row = 0; row < 5; row++)
            {
                for (int column = 0; column < 5; column++)
                {
                    table[row, column] = 'x';
                }
            }

            return table;
        }
    }
}