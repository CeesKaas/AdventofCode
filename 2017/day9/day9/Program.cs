using day9.implementation;
using System;
using System.IO;

namespace day9
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = File.ReadAllText("input.txt");
            Console.WriteLine(StreamParser.ScoreStream(input));
            Console.Read();
        }
    }
}
