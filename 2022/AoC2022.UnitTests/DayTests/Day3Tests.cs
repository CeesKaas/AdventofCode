using AoC2022.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2022.UnitTests.DayTests;

internal class Day3Tests
{
    [Test]
    public async Task TestDay3Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day3(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(157));
    }
    [Test]
    public async Task TestDay3Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day3(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(70));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw"));
        return inputMock;
    }
}
