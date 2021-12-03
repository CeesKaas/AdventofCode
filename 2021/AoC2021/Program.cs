using AoC2021.Days;
using System.Diagnostics;

Console.WriteLine("Starting");
var sw = Stopwatch.StartNew();
await new Day3().Start();
sw.Stop();
Console.WriteLine($"Finished executions took: {sw.Elapsed}");