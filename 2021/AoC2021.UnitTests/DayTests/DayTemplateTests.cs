using AoC2021.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2021.UnitTests.DayTests;

internal class DayTemplateTests
{
    [Test]
    public async Task TestDayTemplatePart1()
    {
        var inputMock = new Mock<IInputFetcher>();
        var objUnderTest = new DayTemplate(inputMock.Object);
        PrepareInput(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(0));
    }
    [Test]
    public async Task TestDayTemplatePart2()
    {
        var inputMock = new Mock<IInputFetcher>();
        var objUnderTest = new DayTemplate(inputMock.Object);
        PrepareInput(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(0));
    }

    private static void PrepareInput(Mock<IInputFetcher> inputMock)
    {
        inputMock.Setup(m => m.GetTransformedSplitInputForDay(1, It.IsAny<Func<string, int>>())).Returns(Task.FromResult((ICollection<int>)new int[0]));
    }
}
