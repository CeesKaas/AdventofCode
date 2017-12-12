using System;
using System.Collections.Generic;
using System.Text;

namespace day2.implemenation
{
    public static class Utils
    {
        public static int[] ExtractArrayFromString(string inputString)
        {
            string[] items = inputString.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] input = new int[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                input[i] = int.Parse(items[i]);
            }
            return input;
        }
        public static int[][] ExtractSpreadSheetFromString(string inputString)
        {
            var lines = inputString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            int[][] output = new int[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                output[i] = ExtractArrayFromString(lines[i]);
            }
            return output;
        }
    }
}
