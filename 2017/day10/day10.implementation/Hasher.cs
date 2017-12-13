using System;
using System.Linq;
using System.Text;

namespace day10.implementation
{
    public class Hasher
    {
        public static byte[] DoWork(int listSize, byte[] steps)
        {
            byte[] list = Enumerable.Range(0, listSize).Select(_ => (byte)_).ToArray();
            var currentPosition = 0;
            for (int i = 0; i < steps.Length; i++)
            {
                var lengthToTake = steps[i];

                for (int j = 0; j < lengthToTake / 2; j++)
                {
                    int leftsideIndex = (currentPosition + j) % listSize;
                    int rightSideIndex = (currentPosition + (lengthToTake - (j + 1))) % listSize;
                    byte temp = list[leftsideIndex];
                    list[leftsideIndex] = list[rightSideIndex];
                    list[rightSideIndex] = temp;
                }
                currentPosition += steps[i] + i;
                currentPosition %= listSize;
            }

            return list;
        }
        public static byte[] DoWork2(int listSize, int rounds, string input)
        {
            byte[] list = Enumerable.Range(0, listSize).Select(_ => (byte)_).ToArray();
            byte[] steps = Encoding.ASCII.GetBytes(input).Concat(new byte[] { 17, 31, 73, 47, 23 }).ToArray();

            var currentPosition = 0;
            var skip = 0;
            for (int k = 0; k < rounds; k++)
            {
                for (int i = 0; i < steps.Length; i++)
                {
                    var lengthToTake = steps[i];

                    for (int j = 0; j < lengthToTake / 2; j++)
                    {
                        int leftsideIndex = (currentPosition + j) % listSize;
                        int rightSideIndex = (currentPosition + (lengthToTake - (j + 1))) % listSize;
                        byte temp = list[leftsideIndex];
                        list[leftsideIndex] = list[rightSideIndex];
                        list[rightSideIndex] = temp;
                    }
                    currentPosition += steps[i] + skip;
                    skip++;
                    currentPosition %= listSize;
                }
            }
            byte[] result = new byte[16];

            for (int i = 0; i < list.Length; i++)
            {
                result[i / 16] ^= list[i];
            }

            return result;
        }
    }
}
