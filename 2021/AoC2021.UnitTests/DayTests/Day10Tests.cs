using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests.DayTests;

internal class Day10Tests
{
    [Test]
    public async Task TestDay10Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day10(inputMock.Object);
        Assert.That((await objUnderTest.Part1And2()).partOne, Is.EqualTo(26397));
    }
    [Test]
    public async Task TestDay10Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day10(inputMock.Object);
        Assert.That((await objUnderTest.Part1And2()).partTwo, Is.EqualTo(288957));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]"));
        return inputMock;
    }
}
