using day17.implementation;
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
        [TestCase(3)]
        [TestCase(376)]
        public void Test1(int stepSize)
        {
            var list = new SpinLocker(stepSize);
            list.BuildList(2017);
            var index = list.result.IndexOf(2017);
            if (stepSize == 3)
            {
                Assert.That(list.result[index + 1], Is.EqualTo(638));
            }
            else
            {
                Assert.Pass(list.result[index + 1].ToString());
            }
        }
        [Test]
        [TestCase(3,2017)]
        public void Test1(int stepSize,int steps)
        {
            var list = new SpinLocker(stepSize);
            list.BuildList(steps);
            var index = list.result.IndexOf(2017);
            var indexOfZero = list.result.IndexOf(0);
            if (stepSize == 3)
            {
                Assert.That(list.result[index + 1], Is.EqualTo(638));
            }
            else
            {
                Assert.Pass(list.result[index + 1].ToString() + " " + list.result[indexOfZero + 1]);
            }
        }

        [Test]
        [TestCase(0, "(0)")]
        [TestCase(1, "0 (1)")]
        [TestCase(2, "0 (2) 1")]
        [TestCase(3, "0  2 (3) 1")]
        [TestCase(4, "0  2 (4) 3  1")]
        [TestCase(5, "0 (5) 2  4  3  1")]
        [TestCase(6, "0  5  2  4  3 (6) 1")]
        [TestCase(7, "0  5 (7) 2  4  3  6  1")]
        [TestCase(8, "0  5  7  2  4  3 (8) 6  1")]
        [TestCase(9, "0 (9) 5  7  2  4  3  8  6  1")]
        public void Test2(int steps, string expectedList)
        {
            var list = new SpinLocker(3);
            list.BuildList(steps);
            Assert.That(string.Join("", list.result.Select(_ => $"{(_==steps?"(":" ")}{_}{(_ == steps ? ")" : " ")}")).Trim(), Is.EqualTo(expectedList));
        }
    }
}