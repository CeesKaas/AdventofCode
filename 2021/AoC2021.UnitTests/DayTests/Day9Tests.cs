using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests.DayTests;

internal class Day9Tests
{
    [Test]
    public async Task TestDay9Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day9(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(15));
    }
    [Test]
    public async Task TestDay9Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day9(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(0));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"2199943210
3987894921
9856789892
8767896789
9899965678"));
        return inputMock;
    }
}
