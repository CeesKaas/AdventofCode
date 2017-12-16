using day13.implementation;
using NUnit.Framework;
using System;

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
            var firewall = new Firewall(@"0: 3
1: 2
4: 4
6: 4");
            Packet packet = null;
            firewall.FinishedPacket += (sender, p) =>
            {
                if (p == null) return;
                packet = p;
                Console.WriteLine($"packet id {p.Id}: {p.RiskScore}, {p.Hit}");
            };
            
            for (int i = 0; i < 50; i++)
                {
                firewall.InjectPacket(new Packet() { Id = i });
            }
            int limiter = 100;
            int packetCount = 0;
            while (packet == null || packet.RiskScore > 0 || packet.Hit > 0)
            {
                firewall.Tick();
                if (limiter--==0)
                {
                    Assert.Fail("Took more than the expected ticks");
                }
            }
            //Assert.That(packet.RiskScore,Is.EqualTo(24));
        }
    }
}