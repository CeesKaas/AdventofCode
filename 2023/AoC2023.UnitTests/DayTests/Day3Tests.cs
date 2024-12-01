using AoC2023.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2023.UnitTests.DayTests;

internal class Day3Tests
{
    [Test]
    public async Task TestDay3Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day3(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(4361));
    }
    [Test]
    public async Task TestDay3Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day3(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(467835));
    }

    private static InputFetcher PrepareInput()
    {
        var inputMock = Substitute.ForPartsOf<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).ReturnsForAnyArgs(Task.FromResult(@"467..114..
...*......
..35..633.
......#...
617*......
.....+.58.
..592.....
......755.
...$.*....
.664.598.."));
        return inputMock;
    }
}
