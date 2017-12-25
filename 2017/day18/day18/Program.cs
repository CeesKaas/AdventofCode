using day18.implementation;
using System;
using System.IO;
using System.Threading.Tasks;

namespace day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var p0 = SoundProgram.Parse(0, input);
            var p1 = SoundProgram.Parse(1, input);

            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() => p0.Execute(p1.OutputQueue, p1.WaitingForInput));
            tasks[1] = Task.Factory.StartNew(() => p1.Execute(p0.OutputQueue, p0.WaitingForInput));

            Task.WaitAll(tasks);
            Console.WriteLine($"0: sent: {p0.Registers["SentNumbers"]}, received: {p0.Registers["ReceivedNumbers"]}");
            Console.WriteLine($"1: sent: {p1.Registers["SentNumbers"]}, received: {p1.Registers["ReceivedNumbers"]}");
            Console.WriteLine(p1.SentNumbers);

            Console.Read();
        }
    }
}
