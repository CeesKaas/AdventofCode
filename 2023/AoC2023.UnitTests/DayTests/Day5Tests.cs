using AoC2023.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Utilities;

namespace AoC2023.UnitTests.DayTests;

internal class Day5Tests
{
    [Test]
    public void TestDay5Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day5(inputMock);
        Assert.That(objUnderTest.Part1(), Is.EqualTo(35));
    }
    [Test]
    public void TestDay5Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day5(inputMock);
        Assert.That(objUnderTest.Part2(), Is.EqualTo(46));
    }
    [Test]
    public void TestDay5Part2Real()
    {
        var inputMock = PrepareRealInput();
        var objUnderTest = new Day5(inputMock);
        Assert.That(objUnderTest.Part2(), Is.EqualTo(137516820));
    }

    private static InputFetcher PrepareInput()
    {
        var inputMock = Substitute.ForPartsOf<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).ReturnsForAnyArgs(Task.FromResult(@"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4"));
        return inputMock;
    }

    private static InputFetcher PrepareRealInput()
    {
        var inputMock = Substitute.ForPartsOf<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).ReturnsForAnyArgs(Task.FromResult(File.ReadAllText(@"C:\src\adventofcode\2023\inputs\day5.input")));
        return inputMock;
    }
}
