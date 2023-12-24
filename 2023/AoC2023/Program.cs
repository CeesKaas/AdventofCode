﻿using AoC2023.Days;
using System.Diagnostics;

Console.WriteLine("Starting");
var sw = Stopwatch.StartNew();
await new Day7().Start();
sw.Stop();
Console.WriteLine($"Finished executions took: {sw.Elapsed}");