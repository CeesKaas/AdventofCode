using AoC2023.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2023.UnitTests.DayTests;

internal class Day7Tests
{
    [Test]
    public async Task TestDay7Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day7(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(6440));
    }
    [Test]
    public async Task TestDay7Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day7(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(5905));
    }

    private static InputFetcher PrepareInput()
    {
        var inputMock = Substitute.ForPartsOf<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).ReturnsForAnyArgs(Task.FromResult(@"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483"));
        return inputMock;
    }
}
