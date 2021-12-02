using AoC2020.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2020.UnitTests.DayTests;

internal class Day1Tests
{
    [Test]
    public async Task TestDay1Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day1(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(514579));
    }
    [Test]
    public async Task TestDay1Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day1(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(241861950));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(1)).Returns(Task.FromResult(@"1721
979
366
299
675
1456"));
        return inputMock;
    }
}
