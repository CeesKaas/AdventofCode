using AoC2024.Days;
using System.Diagnostics;

Console.WriteLine("Starting");
var sw = Stopwatch.StartNew();
await new Day2().Start();
sw.Stop();
Console.WriteLine($"Finished executions took: {sw.Elapsed}");