using AoC2022.Days;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities;

namespace AoC2022.UnitTests.DayTests;

internal class Day7Tests
{
    [Test]
    public async Task TestDay7Part1()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day7(inputMock.Object);
        await objUnderTest.Parse();
        Assert.That(objUnderTest.Part1(), Is.EqualTo(95437));
    }

    [Test]
    public async Task TestDay7Part2()
    {
        var inputMock = PrepareInput();
        var objUnderTest = new Day7(inputMock.Object);
        await objUnderTest.Parse();
        Assert.That(objUnderTest.Part2(), Is.EqualTo(24933642));
    }

    private static Mock<InputFetcher> PrepareInput()
    {
        var inputMock = new Mock<InputFetcher> { CallBase = true };
        inputMock.Setup(m => m.FetchInputAsString(It.IsAny<int>())).Returns(Task.FromResult(@"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k"));
        return inputMock;
    }
}
