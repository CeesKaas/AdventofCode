using AoC2024.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2024.UnitTests.DayTests;

internal class Day2Tests
{
    [Test]
    public async Task TestDay2Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day2(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(2));
    }
    [Test]
    public async Task TestDay2Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day2(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(4));
    }
    [TestCase("7 6 4 2 1", true)]
    [TestCase("1 2 7 8 9", false)]
    [TestCase("9 7 6 2 1", false)]
    [TestCase("1 3 2 4 5", true)]
    [TestCase("8 6 4 4 1", true)]
    [TestCase("1 3 6 7 9", true)]
    [TestCase("47 45 46 47 49", true)]
    public async Task TestDay2Part2_individual(string input, bool safeExpected)
    {
        Assert.That(Day2.testItem(input), Is.EqualTo(safeExpected));
    }

    private static InputFetcher PrepareInput()
    {
        var inputMock = Substitute.For<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).Returns(Task.FromResult("""
        7 6 4 2 1
        1 2 7 8 9
        9 7 6 2 1
        1 3 2 4 5
        8 6 4 4 1
        1 3 6 7 9
        """));
        inputMock.WhenForAnyArgs(m => m.FetchInputAsStrings(Arg.Any<int>())).CallBase();
        return inputMock;
    }
}
