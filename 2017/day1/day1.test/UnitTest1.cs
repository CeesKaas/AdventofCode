using NUnit.Framework;
using day1.implementation;

namespace Tests
{
    public class Tests
    {
        [Test]
        [TestCaseSource(nameof(GivenExamples))]
        public void Test1(int[] input, int expectedSum)
        {
            Assert.That(Summer.DoWork(input), Is.EqualTo(expectedSum));
        }

        static object[] GivenExamples =
    {
        new object[] { new int[]{1,1,2,2}, 3 },
        new object[] { new int[]{1,1,1,1}, 4 },
        new object[] { new int[]{1,2,3,4}, 0 },
        new object[] { new int[]{9,1,2,1,2,1,2,9}, 9 }
    };
    }
}