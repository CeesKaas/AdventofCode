using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests.DayTests;

internal class Day3Tests
{
    [Test]
    public async Task TestDay3Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day3(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(198));
    }

    [Test]
    public async Task TestDay3Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day3(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(0));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(3)).Returns(Task.FromResult(@"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010"));
        return inputMock;
    }
}
