using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Entities;

public record struct SubmarineCommand(Direction Direction, int Distance);

public static class SubmarineCommandParser
{
    public static SubmarineCommand Parse(string input)
    {
        var parts = input.Trim().Split(' ');
        var direction = (Direction)Enum.Parse(typeof(Direction), parts[0], true);
        var distance = int.Parse(parts[1]);
        return new SubmarineCommand(direction, distance);
    }
}