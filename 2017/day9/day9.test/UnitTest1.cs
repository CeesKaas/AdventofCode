using day9.implementation;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("{}", 1)]
        [TestCase("{{{}}}", 6)]
        [TestCase("{{},{}}", 5)]
        [TestCase("{{{},{},{{}}}}", 16)]
        [TestCase("{<a>,<a>,<a>,<a>}", 1)]
        [TestCase("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9)]
        [TestCase("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9)]
        [TestCase("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3)]
        public void Test1(string input, int expectedScore)
        {
            Assert.AreEqual(expectedScore, StreamParser.ScoreStream(input).score);
        }
        [Test]
        [TestCase("{<>}", 0)]
        [TestCase("{<random characters>}", 17)]
        [TestCase("{<<<<>}", 3)]
        [TestCase("{<{!>}>}", 2)]
        [TestCase("{<!!>}", 0)]
        [TestCase("{<!!!>>}", 0)]
        [TestCase("{<{o\"i!a,<{i<a>}", 10)]
        public void Test2(string input, int expectedScore)
        {
            Assert.AreEqual(expectedScore, StreamParser.ScoreStream(input).removedGarbage);
        }
    }
}