using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Levenshtein_Distance
{
    class Program
    {
        private static string _firstString;
        private static string _secondString;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Input string for operations:");
                Console.Write("First string: ");
                _firstString = Console.ReadLine();
                Console.Write("Second string: ");
                _secondString = Console.ReadLine();
            }
            else
            {
                _firstString = args[0];
                _secondString = args[1];
            }
            var distance = ApplyAlgorithm();
            var concurrence = CalculateConcurrence(distance);
            Console.WriteLine();
            Console.WriteLine("Concurrence: {0}%", concurrence);
            Console.ReadKey();
        }

        private static int ApplyAlgorithm()
        {
            var difference = 0;
            int[,] matrix = new int[_firstString.Length + 1, _secondString.Length + 1];

            for (int i = 0; i <= _firstString.Length; i++)
            {
                matrix[i, 0] = i;
            }
            for (int i = 0; i <= _secondString.Length; i++)
            {
                matrix[0, i] = i;
            }
            for (int i = 1; i <= _firstString.Length; i++)
            {
                for (int j = 1; j <= _secondString.Length; j++)
                {
                    difference = (_firstString[i - 1] == _secondString[j - 1]) ? 0 : 1;
                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
                        matrix[i - 1, j - 1] + difference);
                }
            }
            DisplayMatrix(matrix);
            return matrix[_firstString.Length, _secondString.Length];
        }

        private static double CalculateConcurrence(int distance)
        {
            var concurrence = 0.0;
            concurrence = (distance*100)/_firstString.Length;
            concurrence = Math.Round(concurrence, 2);
            concurrence = 100 - concurrence;
            return concurrence;
        }

        private static void DisplayMatrix(int[,] matrix)
        {
            Console.WriteLine();
            Console.WriteLine("Matrix:");
            Console.Write("{0,7}", _firstString[0]);
            for (int i = 1; i < _firstString.Length; i++)
            {
                Console.Write("{0,3}", _firstString[i]);
            }
            Console.WriteLine();
            Console.Write("{0,1}", " ");
            for (int i = 0; i < _firstString.Length + 1; i++)
            {
                Console.Write("{0,3}", matrix[i, 0]);
            }
            Console.WriteLine();
            for (int i = 1; i < _secondString.Length + 1; i++)
            {
                Console.Write(_secondString[i - 1]);
                for (int j = 0; j < _firstString.Length + 1; j++)
                {
                    if (i == _secondString.Length && j == _firstString.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    Console.Write("{0,3}", matrix[j, i]);
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }
    }
}
