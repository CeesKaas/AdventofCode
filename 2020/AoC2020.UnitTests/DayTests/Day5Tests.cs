using AoC2020.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2020.UnitTests.DayTests;

internal class Day5Tests
{
    [TestCase("FBFBBFFRLR", 357)]
    [TestCase("BFFFBBFRRR", 567)]
    [TestCase("FFFBBBFRRR", 119)]
    [TestCase("BBFFBBFRLL", 820)]
    public void GetSeatIdTest(string input, int expectedResult)
    {
        var objUnderTest = new Day5();
        Assert.That(objUnderTest.GetSeatId(input), Is.EqualTo(expectedResult));
    }

    [Test]
    public async Task TestDay5Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day5(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(820));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"FBFBBFFRLR
BFFFBBFRRR
FFFBBBFRRR
BBFFBBFRLL"));
        return inputMock;
    }
}
