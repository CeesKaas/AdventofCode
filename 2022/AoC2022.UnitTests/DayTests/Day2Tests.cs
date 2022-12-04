using AoC2022.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2022.UnitTests.DayTests;

internal class Day2Tests
{
    [Test]
    public async Task TestDay2Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day2(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(15));
    }
    [Test]
    public async Task TestDay2Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day2(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(12));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"A Y
B X
C Z"));
        return inputMock;
    }
}
