using day8.implementation;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string input = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";
            (var result,int max) = CommandParser.ParseAndExecuteCommands(input);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result.Values.Max(_=>_.Value), Is.EqualTo(1));
            Assert.That(max, Is.EqualTo(10));
        }
    }
}