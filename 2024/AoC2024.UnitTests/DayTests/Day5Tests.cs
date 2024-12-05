using AoC2024.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2024.UnitTests.DayTests;

internal class Day5Tests
{
    [Test]
    public async Task TestDay5Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day5(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(143));
    }
    [Test]
    public async Task TestDay5Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day5(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(0));
    }

    private static InputFetcher PrepareInput()
    {
        var inputMock = Substitute.For<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).Returns(Task.FromResult("""
47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47
        """));
        inputMock.WhenForAnyArgs(m => m.FetchInputAsStrings(Arg.Any<int>())).CallBase();
        return inputMock;
    }
}
