using AoC2020.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2020.UnitTests.DayTests;

internal class Day8Tests
{
    [Test]
    public async Task TestDay8Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day8(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(5));
    }

    [Test]
    public async Task TestDay8Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day8(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(0));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6"));
        return inputMock;
    }
}
