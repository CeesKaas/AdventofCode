using AoC2023.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2023.UnitTests.DayTests;

internal class Day1Tests
{
    [Test]
    public async Task TestDay1Part1()
    {
        var inputMock = PrepareInput(@"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet");
        var objUnderTest = new Day1(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(142));
    }
    [Test]
    public async Task TestDay1Part2()
    {
        var inputMock = PrepareInput(@"two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen");
        var objUnderTest = new Day1(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(281));
    }

    [Test]
    [TestCase("two1nine", new[] { 29 })]
    [TestCase("eightnineseventwo1seven", new[] { 87 })]
    public async Task TestParse(string input, int[] output)
    {
        var inputMock = PrepareInput(input);
        var objUnderTest = new Day1(inputMock);
        Assert.That(await objUnderTest.Parse(), Is.EquivalentTo(output));
    }

    private static InputFetcher PrepareInput(string inputString)
    {
        var inputMock = Substitute.ForPartsOf<InputFetcher>();
        inputMock.FetchInputAsString(1).ReturnsForAnyArgs(Task.FromResult(inputString));
        return inputMock;
    }
}
