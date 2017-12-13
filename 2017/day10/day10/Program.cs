using day10.implementation;
using System;
using System.Linq;

namespace day10
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "102,255,99,252,200,24,219,57,103,2,226,254,1,0,69,216";
            {
                var result = Hasher.DoWork(256, input.Split(',').Select(byte.Parse).ToArray());

                Console.WriteLine(string.Join(' ', result));

                Console.WriteLine(result[0] * result[1]);
            }
            {
                var result = Hasher.DoWork2(256, 64, input);

                Console.WriteLine(string.Join("", result.Select(_ => _.ToString("x2"))));
            }
            Console.Read();
        }
    }
}
