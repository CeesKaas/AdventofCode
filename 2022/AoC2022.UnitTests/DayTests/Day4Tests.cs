using AoC2022.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2022.UnitTests.DayTests;

internal class Day4Tests
{
    [Test]
    public async Task TestDay4Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day4(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(2));
    }
    [Test]
    public async Task TestDay4Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day4(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(4));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8"));
        return inputMock;
    }
}
