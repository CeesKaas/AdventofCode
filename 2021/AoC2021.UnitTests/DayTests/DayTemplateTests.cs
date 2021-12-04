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
        var inputMock = PrepareInput();
        var objUnderTest = new DayTemplate(inputMock.Object);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(0));
    }
    [Test]
    public async Task TestDayTemplatePart2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new DayTemplate(inputMock.Object);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(0));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@""));
        return inputMock;
    }
}
