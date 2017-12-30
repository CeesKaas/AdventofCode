using day19.implementation;
using System;
using System.IO;

namespace day19
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var output = Router.GetRoute(input);

            Console.WriteLine(output);
            Console.Read();
        }
    }
}
