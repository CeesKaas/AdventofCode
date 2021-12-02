using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests;

internal class Day1Tests
{
    [Test]
    public async Task TestDay1Part1()
    {
        var inputMock = new Mock<IInputFetcher>();
        var objUnderTest = new Day1(inputMock.Object);
        PrepareInput(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(7));
    }
    [Test]
    public async Task TestDay1Part2()
    {
        var inputMock = new Mock<IInputFetcher>();
        var objUnderTest = new Day1(inputMock.Object);
        PrepareInput(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(5));
    }

    private static void PrepareInput(Mock<IInputFetcher> inputMock)
    {
        inputMock.Setup(m => m.GetTransformedSplitInputForDay<int>(1, It.IsAny<Func<string, int>>())).Returns(Task.FromResult((ICollection<int>)new int[]{
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263
            }));
    }
}
