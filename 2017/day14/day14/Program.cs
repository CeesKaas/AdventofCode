using day14.implementation;
using System;

namespace day14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var disk = new DiskGrid("amgozmfv");

            foreach (var item in disk.StringData)
            {
                Console.WriteLine(item);
            }
            foreach (var item in disk.StringRegions)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Active Blocks; {disk.ActiveBlocks}");
            Console.WriteLine($"Active Blocks; {disk.ActiveRegions}");

            Console.Read();
        }
        
    }
}
