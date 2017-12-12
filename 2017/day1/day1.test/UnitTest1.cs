using NUnit.Framework;
using day1.implementation;

namespace Tests
{
    public class Tests
    {
        [Test]
        [TestCaseSource(nameof(GivenExamples))]
        public void TestDoWork(int[] input, int expectedSum)
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

        [Test]
        [TestCaseSource(nameof(GivenStringExamples))]
        public void TestStringParser(string input, int[] expectedArray)
        {
            Assert.That(Summer.ExtractArrayFromString(input), Is.EquivalentTo(expectedArray));
        }

        static object[] GivenStringExamples =
        {
            new object[] { "1122",new int[]{1,1,2,2} },
            new object[] { "1111",new int[]{1,1,1,1} },
            new object[] { "1234",new int[]{1,2,3,4} },
            new object[] { "91212129",new int[]{9,1,2,1,2,1,2,9} }
        };
    }
}