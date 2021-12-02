using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Entities;

public class Submarine
{
    public int Horizontal { get; set; }
    public int Depth { get; set; }

    public void Execute(SubmarineCommand submarineCommand)
    {
        switch (submarineCommand.Direction)
        {
            case Direction.Up:
                Depth -= submarineCommand.Distance;
                break;
            case Direction.Down:
                Depth += submarineCommand.Distance;
                break;
            case Direction.Forward:
                Horizontal += submarineCommand.Distance;
                break;
            default:
                throw new InvalidOperationException();
        }
    }
}
