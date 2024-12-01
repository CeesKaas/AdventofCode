using AoC2023.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2023.UnitTests.DayTests;

internal class Day6Tests
{
    [Test]
    public async Task TestDay6Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day6(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(288));
    }
    [Test]
    public async Task TestDay6Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day6(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(71503));
    }

    private static InputFetcher PrepareInput()
    {
        var inputMock = Substitute.ForPartsOf<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).ReturnsForAnyArgs(Task.FromResult(@"Time:      7  15   30
Distance:  9  40  200"));
        return inputMock;
    }
}
