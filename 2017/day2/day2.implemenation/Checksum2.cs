using System;
using System.Linq;

namespace day2.implemenation
{
    public class Checksum2
    {
        private int[][] _spreadsheet;

        public Checksum2(int[][] spreadsheet)
        {
            _spreadsheet = spreadsheet;
        }

        public int Result => CalculateCheckSum(_spreadsheet);

        public static int CalculateCheckSum(int[][] spreadsheet)
        {
            var sum = 0;
            foreach (var row in spreadsheet)
            {
                var done = false;
                for (int i = 0; i < row.Length && !done; i++)
                    for (int j = i+1; j < row.Length && !done; j++)
                    {
                        if (i == j) continue;
                        if (row[i] % row[j] == 0)
                        {
                            sum += row[i] / row[j];
                            done = true;
                        }
                        if (row[j] % row[i] == 0)
                        {
                            sum += row[j] / row[i];
                            done = true;
                        }
                    }
            }
            return sum;
        }

    }
}
