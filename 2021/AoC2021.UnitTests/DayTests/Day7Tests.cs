using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests.DayTests;

internal class Day7Tests
{
    [Test]
    public async Task TestDay7Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day7(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(37.0));
    }
    [Test]
    public async Task TestDay7Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day7(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(168));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"16,1,2,0,4,2,7,1,2,14"));
        return inputMock;
    }
}
