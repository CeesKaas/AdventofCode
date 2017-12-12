using NUnit.Framework;
using day2.implemenation;

namespace Tests
{
    public class Tests
    {
        static object[] GivenExamples =
            {
            new object[] {new int[][] { new[] { 5, 1, 9, 5 }, new[] { 7, 5, 3 }, new[] { 2, 4, 6, 8 }}, 18}};


        [Test]
        [TestCaseSource(nameof(GivenExamples))]
        public void ChecksumResultTest(int[][] input, int expectedChecksum)
        {
            var checksumCalculator = new Checksum(input);
            Assert.That(checksumCalculator.Result, Is.EqualTo(expectedChecksum));
        }
        static object[] GivenExamplesPart2 =
            {
            new object[] {new int[][] { new[] {5,9,2,8 }, new[] { 9,4,7,3 }, new[] { 3,8,6,5}}, 9}};


        [Test]
        [TestCaseSource(nameof(GivenExamplesPart2))]
        public void Checksum2ResultTest(int[][] input, int expectedChecksum)
        {
            var checksumCalculator = new Checksum2(input);
            Assert.That(checksumCalculator.Result, Is.EqualTo(expectedChecksum));
        }
        [Test]
        [TestCase(new[] { 5, 1, 9, 5 }, 1, 9)]
        [TestCase(new[] { 7, 5, 3 }, 3, 7)]
        [TestCase(new[] { 2, 4, 6, 8 }, 2, 8)]
        public void CheckRowMinAndMax(int[] input, int expectedMin, int expectedMax)
        {
            (int min, int max) = Checksum.GetRowMinAndMax(input);
            Assert.That(min, Is.EqualTo(expectedMin));
            Assert.That(max, Is.EqualTo(expectedMax));
        }

        [Test]
        [TestCaseSource(nameof(GivenLineExamples))]
        public void TestLineParser(string input, int[] expectedArray)
        {
            Assert.That(Utils.ExtractArrayFromString(input), Is.EquivalentTo(expectedArray));
        }

        static object[] GivenLineExamples =
        {
            new object[] { "1 1 2 2",new int[]{1,1,2,2} },
            new object[] { "1 1 1 1",new int[]{1,1,1,1} },
            new object[] { "1 2 3 4",new int[]{1,2,3,4} },
            new object[] { "9 1 2 1 2 1 2 9",new int[]{9,1,2,1,2,1,2,9} }
        };

        [Test]
        [TestCaseSource(nameof(GivenSheetsExamples))]
        public void TestSheetParser(string input, int[][] expectedArray)
        {
            Assert.That(Utils.ExtractSpreadSheetFromString(input), Is.EquivalentTo(expectedArray));
        }

        static object[] GivenSheetsExamples =
        {
            new object[] { @"5 1 9 5
7 5 3
2 4 6 8", new int[][]{new []{5,1,9,5},new []{7,5,3},new []{2,4,6,8}}
        } };
    }
}