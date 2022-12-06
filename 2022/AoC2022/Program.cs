using AoC2022.Days;
using System.Diagnostics;

Console.WriteLine("Starting");
var sw = Stopwatch.StartNew();
await new Day6().Start();
sw.Stop();
Console.WriteLine($"Finished executions took: {sw.Elapsed}");