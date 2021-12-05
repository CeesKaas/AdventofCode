using AoC2020.Days;
using System.Diagnostics;

Console.WriteLine("Starting");
var sw = Stopwatch.StartNew();
await new Day8().Start();
sw.Stop();
Console.WriteLine($"Finished executions took: {sw.Elapsed}");