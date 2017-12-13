using NUnit.Framework;
using day7.implementation;
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
        [TestCaseSource(nameof(GivenExample))]
        public void Part1Test(string input, Item[] expectedOutput)
        {
            Assert.That(TowerParser.Parse(input), Is.EquivalentTo(expectedOutput).Using<Item>(new ItemComparer()));
        }
        [Test]
        [TestCaseSource(nameof(GivenExample))]
        public void Part2Test(string input, Item[] expectedOutput)
        {
            var baseItem = TowerParser.Parse(input)[0];

            Assert.That(baseItem.Balanced, Is.False);

            Assert.That(baseItem.Children.Count(_ => _.Balanced), Is.EqualTo(3));
            var foundWeights = baseItem.Children.GroupBy(_ => _.CombinedWeight).OrderBy(_=>_.Key);
            Assert.That(foundWeights.Count(), Is.EqualTo(2));
        }

        [Test]
        [TestCaseSource(nameof(GivenExample))]
        public void TestEqualityComparer(string input, Item[] expectedOutput)
        {
            Assert.That(new[] { Item.Create("tknk", 41,new Item[] { Item.Create("ugml", 68, new Item[] { Item.Create("gyxo",61), Item.Create("ebii",61) , Item.Create("jptl",61) }),
                Item.Create("padx",45, new Item[] { Item.Create("pbga",66), Item.Create("havc",66), Item.Create("qoyq",66) }),
                Item.Create("fwft",72, new Item[] { Item.Create("ktlj",57), Item.Create("cntj",57), Item.Create("xhth",57) }) }) }, Is.EquivalentTo(expectedOutput).Using<Item>(new ItemComparer()));
        }
        static object[] GivenExample =
        {
            new object[] { @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)", new [] { Item.Create("tknk", 41,new Item[] { Item.Create("ugml", 68, new Item[] { Item.Create("gyxo",61), Item.Create("ebii",61) , Item.Create("jptl",61) }),
                Item.Create("padx",45, new Item[] { Item.Create("pbga",66), Item.Create("havc",66), Item.Create("qoyq",66) }),
                Item.Create("fwft",72, new Item[] { Item.Create("ktlj",57), Item.Create("cntj",57), Item.Create("xhth",57) }) }) }
    }
};
    }
}