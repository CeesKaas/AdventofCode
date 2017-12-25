using day18.implementation;
using NUnit.Framework;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        /*[Test]
        public void Test1()
        {
            var p = SoundProgram.Parse(@"set a 1
add a 2
mul a a
mod a 5
snd a
set a 0
rcv a
jgz a -1
set a 1
jgz a -2");
            p.Execute();
            Assert.AreEqual(4, p.LastRecoveredValue);
        }*/
        [Test]
        public void Test1()
        {
            var input = @"snd 1
snd 2
snd p
rcv a
rcv b
rcv c
rcv d";
            var p0 = SoundProgram.Parse(0, input);
            var p1 = SoundProgram.Parse(1, input);

            Task[] tasks = new Task[2];
            tasks[0] = Task.Factory.StartNew(() => p0.Execute(p1.OutputQueue, p1.WaitingForInput));
            tasks[1] = Task.Factory.StartNew(() => p1.Execute(p0.OutputQueue, p0.WaitingForInput));

            Task.WaitAll(tasks);

            Assert.AreEqual(3, (int) p1.SentNumbers);
        }
    }
}