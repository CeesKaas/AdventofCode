using System;

namespace day11.implementation
{
    public class DirectionParser
    {
        public static (double x, double y, int steps, int furthest) Parse(string input)
        {
            string[] steps = input.Trim().Split(',');

            var p = (x: 0.0, y: 0.0, steps: 0, furthest:0);

            var ysidestep = Math.Tan(((2 * Math.PI) / 360) * 30);

            foreach (var step in steps)
            {
                switch (step)
                {
                    case "n":
                        p.y += ysidestep * 2;
                        break;
                    case "ne":
                        p.y += ysidestep;
                        p.x++;
                        break;
                    case "se":
                        p.y -= ysidestep;
                        p.x++;
                        break;
                    case "s":
                        p.y -= ysidestep * 2;
                        break;
                    case "sw":
                        p.y -= ysidestep;
                        p.x--;
                        break;
                    case "nw":
                        p.y += ysidestep;
                        p.x--;
                        break;
                }
                p.furthest = Math.Max(p.furthest, (int)(((Math.Abs(p.y) - (Math.Abs(p.x) * ysidestep)) / (2 * ysidestep)) + Math.Abs(p.x)));
            }

            p.steps = (int)(((Math.Abs(p.y) - (Math.Abs(p.x) * ysidestep)) / (2 * ysidestep)) + Math.Abs(p.x));

            return p;
        }
    }
}
