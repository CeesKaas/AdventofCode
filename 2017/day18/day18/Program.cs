using day18.implementation;
using System;
using System.IO;

namespace day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var p = SoundProgram.Parse(input);
            p.Execute();
            Console.WriteLine(p.LastRecoveredValue);
            Console.Read();
        }
    }
}
