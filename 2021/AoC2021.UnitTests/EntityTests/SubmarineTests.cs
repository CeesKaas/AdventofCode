using AoC2021.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.UnitTests.EntityTests;

internal class SubmarineTests
{
    [Test]
    public void CommandForwardUpdatesHorizontalLocation()
    {
        var sub = new Submarine();
        sub.Execute(new SubmarineCommand(Direction.Forward, 1));
        Assert.That(sub.Horizontal, Is.EqualTo(1));
        Assert.That(sub.Depth, Is.EqualTo(0));
    }

    [Test]
    public void CommandDownUpdatesAim()
    {
        var sub = new Submarine();
        sub.Execute(new SubmarineCommand(Direction.Up, 1));
        Assert.That(sub.Horizontal, Is.EqualTo(0));
        Assert.That(sub.Aim, Is.EqualTo(-1));
    }

    [Test]
    public void CommandUpUpdatesAim()
    {
        var sub = new Submarine();
        sub.Execute(new SubmarineCommand(Direction.Down, 1));
        Assert.That(sub.Horizontal, Is.EqualTo(0));
        Assert.That(sub.Aim, Is.EqualTo(1));
    }

    [Test]
    public void CommandForwardUpdatesHorizontalLocationAndDepthWhenAimIsNotZero()
    {
        var sub = new Submarine { Aim = 1 };
        sub.Execute(new SubmarineCommand(Direction.Forward, 1));
        Assert.That(sub.Horizontal, Is.EqualTo(1));
        Assert.That(sub.Depth, Is.EqualTo(1));
    }
}
