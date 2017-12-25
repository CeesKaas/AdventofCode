using day16.implementation;
using NUnit.Framework;
using System.Diagnostics;

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
            var d = new Dance(5, "s1,x3/4,pe/b");
            var s = Stopwatch.StartNew();
            d.Execute();
            s.Stop();
            System.Console.WriteLine(s.Elapsed);
            Assert.AreEqual("baedc", d.CurrentState);
            s.Reset();
            s.Start();
            d.Execute();
            s.Stop();
            System.Console.WriteLine(s.Elapsed);
            Assert.AreEqual("ceadb", d.CurrentState);
        }
    }
}