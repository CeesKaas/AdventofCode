using day16.implementation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace day16
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
            var foundValues = new List<string>();
            Dance dance = new Dance(16, input);
            var s = Stopwatch.StartNew();
            var bla =(1_000_000_000 % 59);
            for (long i = 0; i < 120; i++)
            {
                dance.Execute();
                Console.WriteLine($"{i,-3} {dance.CurrentState}");
            }
            Console.WriteLine(dance.CurrentState);
            dance.Execute();
            Console.WriteLine(dance.CurrentState);
            Console.Read();
        }
    }
}
