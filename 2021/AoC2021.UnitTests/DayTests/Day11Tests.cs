using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests.DayTests;

internal class Day11Tests
{
    [Test]
    public async Task TestDay11Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day11(inputMock.Object);
        (int partOne, int partTwo) = (await objUnderTest.Part1And2());
        Assert.That(partOne, Is.EqualTo(1656), "Number of flashes is incorrect");
        Assert.That(partTwo, Is.EqualTo(195), "Number of turns before sync is incorrect");
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526"));
        return inputMock;
    }
}
