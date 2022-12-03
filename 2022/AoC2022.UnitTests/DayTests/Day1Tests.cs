using AoC2022.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2022.UnitTests.DayTests;

internal class Day1Tests
{
    [Test]
    public async Task TestDay1Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day1(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(24000));
    }
    [Test]
    public async Task TestDay1Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day1(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(45000));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"1000
2000
3000

4000

5000
6000

7000
8000
9000

10000"));
        return inputMock;
    }
}
