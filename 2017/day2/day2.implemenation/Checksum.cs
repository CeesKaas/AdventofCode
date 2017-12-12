using System;
using System.Linq;

namespace day2.implemenation
{
    public class Checksum
    {
        private int[][] _spreadsheet;

        public Checksum(int[][] spreadsheet)
        {
            _spreadsheet = spreadsheet;
        }

        public int Result => CalculateCheckSum(_spreadsheet);

        public static int CalculateCheckSum(int[][] spreadsheet)
        {
            var sum = 0;
            foreach (var row in spreadsheet)
            {
                (int min, int max) = GetRowMinAndMax(row);
                sum += max - min;

            }
            return sum;
        }

        public static (int min, int max) GetRowMinAndMax(int[] row)
        {
            return row.Aggregate((min: int.MaxValue, max: int.MinValue), (accumulate, item) => (min: Math.Min(accumulate.min, item), max: Math.Max(accumulate.max, item)));
        }
    }
}
