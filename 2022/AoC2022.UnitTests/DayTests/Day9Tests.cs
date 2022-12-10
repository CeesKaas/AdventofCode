using AoC2022.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2022.UnitTests.DayTests;

internal class Day9Tests
{
    [Test]
    public async Task TestDay9Part1()
    {
        var inputMock = PrepareInput(@"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2");
        var objUnderTest = new Day9(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(13));
    }
    [TestCase(@"R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2",1)]
    [TestCase(@"R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20", 36)]
    public async Task TestDay9Part2(string input, int expected)
    {
        var inputMock = PrepareInput(input);
        var objUnderTest = new Day9(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(expected));
    }

    private static Mock<InputFetcher> PrepareInput(string input)
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(input));
        return inputMock;
    }
}
