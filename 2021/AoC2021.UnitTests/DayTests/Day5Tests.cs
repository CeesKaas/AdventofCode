using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests.DayTests;

internal class Day5Tests
{
    [Test]
    public async Task TestDay5Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day5(inputMock.Object);
        Assert.That((await objUnderTest.Part1()).Item1, Is.EqualTo(5));
        Assert.That((await objUnderTest.Part1()).Item2, Is.EqualTo(12));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2"));
        return inputMock;
    }
}
