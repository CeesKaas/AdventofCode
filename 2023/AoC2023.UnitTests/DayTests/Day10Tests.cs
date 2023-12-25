using AoC2023.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2023.UnitTests.DayTests;

internal class Day10Tests
{
    [Test]
    [TestCase(@".....
.S-7.
.|.|.
.L-J.
.....", 4)]
    [TestCase(@"..F7.
.FJ|.
SJ.L7
|F--J
LJ...", 8)]
    public async Task TestDay10Part1(string input, int furthestDistance)
    {
        var inputMock = PrepareInput(input);
        var objUnderTest = new Day10(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(furthestDistance));
    }
    [Test]
    public async Task TestDay10Part2()
    {
        var inputMock = PrepareInput(@".F----7F7F7F7F-7....
.|F--7||||||||FJ....
.||.FJ||||||||L7....
FJL7L7LJLJ||LJ.L-7..
L--J.L7...LJS7F-7L7.
....F-J..F7FJ|L7L7L7
....L7.F7||L7|.L7L7|
.....|FJLJ|FJ|F7|.LJ
....FJL-7.||.||||...
....L---J.LJ.LJLJ...");
        var objUnderTest = new Day10(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(8));
    }

    private static InputFetcher PrepareInput(string input)
    {
        var inputMock = Substitute.ForPartsOf<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).ReturnsForAnyArgs(Task.FromResult(input));
        return inputMock;
    }
}
