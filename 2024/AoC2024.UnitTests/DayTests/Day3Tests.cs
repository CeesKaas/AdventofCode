using AoC2024.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2024.UnitTests.DayTests;

internal class Day3Tests
{
    [Test]
    public async Task TestDay3Part1()
    {
        var inputMock = PrepareInput("xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))");
        var objUnderTest = new Day3(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(161));
    }
    [Test]
    public async Task TestDay3Part2()
    {
        var inputMock = PrepareInput("xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))");
        var objUnderTest = new Day3(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(48));
    }

    private static InputFetcher PrepareInput(string input)
    {
        var inputMock = Substitute.For<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).Returns(Task.FromResult(input));
        inputMock.WhenForAnyArgs(m => m.FetchInputAsStrings(Arg.Any<int>())).CallBase();
        return inputMock;
    }
}
