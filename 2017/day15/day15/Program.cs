using day15.implementation;
using System;

namespace day15
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var left = new RandomNumberGenerator(16807, 634);
                var right = new RandomNumberGenerator(48271, 301);
                var counter = 0;
                for (int i = 0; i < 40_000_000; i++)
                {
                    if (((ushort)right.Next()) == ((ushort)left.Next()))
                    {
                        counter++;
                    }
                }
                Console.WriteLine(counter);
            }
            {
                var left = new RandomNumberGeneratorSlower(16807, 634, 4);
                var right = new RandomNumberGeneratorSlower(48271, 301, 8);
                var counter = 0;
                for (int i = 0; i < 5_000_000; i++)
                {
                    if (((ushort)right.Next()) == ((ushort)left.Next()))
                    {
                        counter++;
                    }
                }
                Console.WriteLine(counter);
            }
            Console.Read();
        }
    }
}
