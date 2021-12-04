using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests.DayTests;

internal class Day4Tests
{
    [Test]
    public async Task TestDay4Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day4(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(4512));
    }
    [Test]
    public async Task TestDay4Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day4(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(1924));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1

22 13 17 11  0
 8  2 23  4 24
21  9 14 16  7
 6 10  3 18  5
 1 12 20 15 19

 3 15  0  2 22
 9 18 13 17  5
19  8  7 25 23
20 11 10 24  4
14 21 16 12  6

14 21 17 24  4
10 16 15  9 19
18  8 23 26 20
22 11 13  6  5
 2  0 12  3  7
"));
        return inputMock;
    }
}
