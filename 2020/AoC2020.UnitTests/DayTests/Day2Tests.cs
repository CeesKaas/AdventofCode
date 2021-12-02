using AoC2020.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2020.UnitTests.DayTests;

internal class Day2Tests
{
    [Test]
    public async Task TestDay2Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day2(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(2));
    }
    [Test]
    public async Task TestDay2Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day2(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(1));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(2)).Returns(Task.FromResult(@"1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc"));
        return inputMock;
    }
}
