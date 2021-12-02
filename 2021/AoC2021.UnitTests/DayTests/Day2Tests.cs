using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests;

internal class Day2Tests
{
    [Test]
    public async Task TestDay2Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day2(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(150));
    }
    [Test]
    public async Task TestDay2Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day2(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(0));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(2)).Returns(Task.FromResult(@"forward 5
down 5
forward 8
up 3
down 8
forward 2"));
        return inputMock;
    }
}
