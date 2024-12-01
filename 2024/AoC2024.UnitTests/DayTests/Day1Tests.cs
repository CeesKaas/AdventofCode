using AoC2024.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2024.UnitTests.DayTests;

internal class Day1Tests
{
    [Test]
    public async Task TestDay1Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day1(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(11));
    }
    [Test]
    public async Task TestDay1Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day1(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(31));
    }

    private static InputFetcher PrepareInput()
    {
        var inputMock = Substitute.For<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).ReturnsForAnyArgs("""
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        """);
        inputMock.WhenForAnyArgs(m => m.FetchInputAsStrings(Arg.Any<int>())).CallBase();
        return inputMock;
    }
}
