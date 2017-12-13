using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputString = "14 0 15 12 11 11 3 5 1 6 8 4 9 1 8 4";
            //var inputString = "0 2 7 0";
            var banks = ExtractArrayFromString(inputString);

            int i = 0;
            int stepsTaken = 0;
            var currentPattern = inputString;
            List<string> seenPatterns = new List<string>();
            while (!seenPatterns.Contains(currentPattern))
            {
                seenPatterns.Add(currentPattern);
                stepsTaken++;
                var max = FindMax(banks);
                var blocks = banks[max];
                banks[max] = 0;
                for (int j = (max + 1) % banks.Length; blocks > 0; j = (j + 1) % banks.Length)
                {
                    banks[j] += 1;
                    blocks--;
                }
                currentPattern = string.Join(' ', banks);
                Console.WriteLine(currentPattern);
                //Thread.Sleep(1000);
            }
            Console.WriteLine(stepsTaken);
            Console.WriteLine(stepsTaken - seenPatterns.IndexOf(currentPattern));
            Console.Read();
        }

        private static int FindMax(int[] banks)
        {
            int max = 0;
            int maxIndex = 0;
            for (int i = 0; i < banks.Length; i++)
            {
                if (banks[i] > max)
                {
                    max = banks[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        public static int[] ExtractArrayFromString(string inputString)
        {
            string[] items = inputString.Split(new[] { '\t', ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] input = new int[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                input[i] = int.Parse(items[i]);
            }
            return input;
        }
    }
}