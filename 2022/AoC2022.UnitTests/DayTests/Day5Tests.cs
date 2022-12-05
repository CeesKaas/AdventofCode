using AoC2022.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2022.UnitTests.DayTests;

internal class Day5Tests
{
    [Test]
    public async Task TestDay5Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day5(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo("CMZ"));
    }
    [Test]
    public async Task TestDay5Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day5(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo("MCD"));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2"));
        return inputMock;
    }
}
