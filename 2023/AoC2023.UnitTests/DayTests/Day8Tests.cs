using AoC2023.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2023.UnitTests.DayTests;

internal class Day8Tests
{
    [Test]
    [TestCase(@"RL

AAA = (BBB, CCC)
BBB = (DDD, EEE)
CCC = (ZZZ, GGG)
DDD = (DDD, DDD)
EEE = (EEE, EEE)
GGG = (GGG, GGG)
ZZZ = (ZZZ, ZZZ)", 2)]
    [TestCase(@"LLR

AAA = (BBB, BBB)
BBB = (AAA, ZZZ)
ZZZ = (ZZZ, ZZZ)", 6)]
    public async Task TestDay8Part1(string input, int expectedSteps)
    {
        var inputMock = PrepareInput(input);
        var objUnderTest = new Day8(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(expectedSteps));
    }

    [Test]
    [TestCase(@"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)", 6)]
    public async Task TestDay8Part2(string input, int expectedSteps)
    {
        var inputMock = PrepareInput(input);
        var objUnderTest = new Day8(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(6));
    }

    private static InputFetcher PrepareInput(string input)
    {
        var inputMock = Substitute.ForPartsOf<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).ReturnsForAnyArgs(Task.FromResult(input));
        return inputMock;
    }
}
