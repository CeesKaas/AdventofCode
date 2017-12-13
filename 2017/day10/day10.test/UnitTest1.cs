using day10.implementation;
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
        [TestCase(12, 5, new byte[] { 3, 4, 1, 5 })]
        public void Test1(int expected, int listLenght, byte[] steps)
        {
            var result = Hasher.DoWork(listLenght, steps);
            Assert.AreEqual(expected, result[0] * result[1]);
        }
        [Test]
        [TestCase("a2582a3a0e66e6e86e3812dcb672a272", "")]
        [TestCase("33efeb34ea91902bb2f59c9920caa6cd", "AoC 2017")]
        [TestCase("3efbe78a8d82f29979031a4aa0b16a9d", "1,2,3")]
        [TestCase("63960835bcdc130f0b66d7ff4f6a5a8e", "1,2,4")]
        public void Test1(string expected, string input)
        {
            var result = Hasher.DoWork2(256,64,input);
            Assert.AreEqual(expected, string.Join("",result.Select(_=>_.ToString("x2"))));
        }

        
    }
}