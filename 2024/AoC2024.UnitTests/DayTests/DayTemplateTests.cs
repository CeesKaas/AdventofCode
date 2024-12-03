using AoC2024.Days;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2024.UnitTests.DayTests;

internal class DayTemplateTests
{
    [Test]
    public async Task TestDayTemplatePart1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new DayTemplate(inputMock);
        Assert.That(await objUnderTest.Part1(), Is.EqualTo(0));
    }
    [Test]
    public async Task TestDayTemplatePart2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new DayTemplate(inputMock);
        Assert.That(await objUnderTest.Part2(), Is.EqualTo(0));
    }

    private static InputFetcher PrepareInput()
    {
        var inputMock = Substitute.For<InputFetcher>();
        inputMock.FetchInputAsString(Arg.Any<int>()).Returns(Task.FromResult("""
        """));
        inputMock.WhenForAnyArgs(m => m.FetchInputAsStrings(Arg.Any<int>())).CallBase();
        return inputMock;
    }
}
