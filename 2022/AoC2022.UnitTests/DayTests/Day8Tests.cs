using AoC2022.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2022.UnitTests.DayTests;

internal class Day8Tests
{
    [Test]
    public async Task TestDay8Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day8(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(21));
    }

    [Test]
    public async Task TestDay8Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day8(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(8));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"30373
25512
65332
33549
35390"));
        return inputMock;
    }
}
