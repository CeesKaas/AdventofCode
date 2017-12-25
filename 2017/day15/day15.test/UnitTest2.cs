using day15.implementation;
using NUnit.Framework;

namespace Tests
{
    public class Tests2
    {
        const int _iterations = 5_000_000;
        int[] _leftNumbers = new int[_iterations];
        int[] _rightNumbers = new int[_iterations];

        [OneTimeSetUp]
        public void Setup()
        {
            var left = new RandomNumberGeneratorSlower(16807, 65, 4);
            var right = new RandomNumberGeneratorSlower(48271, 8921, 8);
            for (int i = 0; i < _iterations; i++)
            {
                _rightNumbers[i] = right.Next();
                _leftNumbers[i] = left.Next();
            }
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
            Assert.AreEqual(309, counter);
        }
    }
}