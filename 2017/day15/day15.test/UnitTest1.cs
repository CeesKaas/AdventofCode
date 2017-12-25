using day15.implementation;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        const int _iterations = 40_000_000;
        int[] _leftNumbers = new int[_iterations];
        int[] _rightNumbers = new int[_iterations];

        [OneTimeSetUp]
        public void Setup()
        {
            var left = new RandomNumberGenerator(16807, 65);
            var right = new RandomNumberGenerator(48271, 8921);
            for (int i = 0; i < _iterations; i++)
            {
                _rightNumbers[i] = right.Next();
                _leftNumbers[i] = left.Next();
            }
        }

        [Test]
        public void CheckInitialLeftNumbers()
        {
            Assert.AreEqual(1092455, _leftNumbers[0]);
            Assert.AreEqual(1181022009, _leftNumbers[1]);
            Assert.AreEqual(245556042, _leftNumbers[2]);
            Assert.AreEqual(1744312007, _leftNumbers[3]);
            Assert.AreEqual(1352636452, _leftNumbers[4]);
        }

        [Test]
        public void CheckInitialRightNumbers()
        {
            Assert.AreEqual(430625591, _rightNumbers[0]);
            Assert.AreEqual(1233683848, _rightNumbers[1]);
            Assert.AreEqual(1431495498, _rightNumbers[2]);
            Assert.AreEqual(137874439, _rightNumbers[3]);
            Assert.AreEqual(285222916, _rightNumbers[4]);
        }
        [Test]
        public void CountEqualLowerParts()
        {
            var counter = 0;
            for (int i = 0; i < _iterations; i++)
            {
                if (((ushort)_leftNumbers[i]) == ((ushort)_rightNumbers[i]))
                {
                    counter++;
                }
            }
            Assert.AreEqual(588, counter);
        }
    }
}