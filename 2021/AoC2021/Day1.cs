using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021
{
    public class Day1
    {
        private readonly IInputFetcher _inputFetcher;

        public Day1(IInputFetcher? inputFetcher = null)
        {
            _inputFetcher = inputFetcher ?? new InputFetcher();
        }
        internal void Start()
        {
            Console.WriteLine($"Day 1 part 1 answer: {Part1()}");
            Console.WriteLine($"Day 1 part 2 answer: {Part2()}");
        }

        public int Part1()
        {
            var input = _inputFetcher.GetTransformedSplitInputForDay(1, int.Parse).ToList();
            int count = 0;
            for (int i = 1; i < input.Count; i++)
            {
                if (input[i - 1] < input[i])
                    count++;
            }
            return count;
        }

        public int Part2()
        {
            var input = _inputFetcher.GetTransformedSplitInputForDay(1, int.Parse).ToList();
            var sumsByThree = SumsByThree(input).ToList();
            int count = 0;
            for (int i = 1; i < sumsByThree.Count; i++)
            {
                if (sumsByThree[i - 1] < sumsByThree[i])
                    count++;
            }
            return count;
        }

        private IEnumerable<int> SumsByThree(List<int> input)
        {
            for (int i = 0; i < input.Count - 2; i++)
            {
                yield return input[i] + input[i + 1] + input[i + 2];
            }
        }
    }
}
