using AoC2023.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2023.UnitTests.DayTests;

internal class Day9Tests
{
    [Test]
    public async Task TestDay9Part1()
    {
        var inputMock = PrepareInput(@"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45");
        var objUnderTest = new Day9(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(114));
    }
    [Test]
    public async Task TestDay9Part2()
    {
        var inputMock = PrepareInput("10  13  16  21  30  45");
        var objUnderTest = new Day9(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(5));
    }

    private static InputFetcher PrepareInput(string input)
    {
        var inputMock = Substitute.ForPartsOf<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).ReturnsForAnyArgs(Task.FromResult(input));
        return inputMock;
    }
}
