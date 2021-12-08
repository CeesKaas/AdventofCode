using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests.DayTests;

internal class Day6Tests
{
    [Test]
    public async Task TestDay6Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day6(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(5934));
    }
    [Test]
    public async Task TestDay6Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day6(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(26984457539));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"3,4,3,1,2"));
        return inputMock;
    }
}
