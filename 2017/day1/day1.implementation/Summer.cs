using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace day1.implementation
{
    public class Summer
    {
        public static int DoWork(ICollection<int> input)
        {
            var aggregate = input.Circular().Aggregate((sum: 0, last: -1), (accumulate, current) =>
                {
                    if (accumulate.last == current)
                    {
                        accumulate.sum += current;
                    }
                    accumulate.last = current;
                    return accumulate;
                });
            return aggregate.sum;
        }

        public static int[] ExtractArrayFromString(string inputString)
        {
            int[] input = new int[inputString.Length];
            for (int i = 0; i < inputString.Length; i++)
            {
                input[i] = inputString[i] - '0';
            }
            return input;
        }
    }

    internal static class CircularCollectionExtensions
    {
        public static IEnumerable<T> Circular<T>(this ICollection<T> input)
        {
            foreach (var i in input)
            {
                yield return i;
            }
            yield return input.First();
        }
    }
}
