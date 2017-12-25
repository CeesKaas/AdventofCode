using day14.implementation;
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
            var disk = new DiskGrid("flqrgnkx");

            string[] data = disk.StringData.Take(8).ToArray();
            Assert.That(data[0].StartsWith("##.#.#.."), Is.True);
            Assert.That(data[1].StartsWith(".#.#.#.#"), Is.True);
            Assert.That(data[2].StartsWith("....#.#."), Is.True);
            Assert.That(data[3].StartsWith("#.#.##.#"), Is.True);
            Assert.That(data[4].StartsWith(".##.#..."), Is.True);
            Assert.That(data[5].StartsWith("##..#..#"), Is.True);
            Assert.That(data[6].StartsWith(".#...#.."), Is.True);
            Assert.That(data[7].StartsWith("##.#.##."), Is.True);



            Assert.That(disk.ActiveBlocks, Is.EqualTo(8108));

            Assert.That(disk.ActiveRegions, Is.EqualTo(1242));

        }

        
    }
}