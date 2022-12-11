using AoC2022.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2022.UnitTests.DayTests;

internal class Day11Tests
{
    [Test]
    public async Task TestDay11Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day11(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(10605));
    }
    [Test]
    public async Task TestDay11Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day11(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(2713310158l));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1"));
        return inputMock;
    }
}
