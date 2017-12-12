using System;
using System.Linq;
using System.Collections.Generic;

namespace day3
{
    class Program
    {
        private const int numberOfItemsInSpiral = 500;

        public enum Direction
        {
            Right,
            Up,
            Left,
            Down
        }

        public struct PositionedNumber
        {
            public int x;
            public int y;
            public int number;

            public static implicit operator PositionedNumber((int x, int y, int number) input)
            {
                return new PositionedNumber { x = input.x, y = input.y, number = input.number };
            }
            public override string ToString()
            {
                return $"{number}:[x:{x} y:{y}]";
            }
        }
        static void Main(string[] args)
        {
            const int input = 347991;
            var offset = (int) Math.Ceiling(Math.Sqrt(numberOfItemsInSpiral)/2);

            Dictionary<int, Dictionary<int, int>> grid = new Dictionary<int, Dictionary<int, int>>();

            for (int i = (offset + 1 )* -1; i < (offset+1); i++)
            {
                grid[i] = new Dictionary<int, int>();
                for (int j = (offset + 1) * -1; j < (offset + 1); j++)
                {
                    grid[i][j] = 0;
                }
            }

            var positions = new PositionedNumber[numberOfItemsInSpiral];

            var p = (x: 0, y: 0, width: 1, d: Direction.Right);
            for (int i = 0; i < numberOfItemsInSpiral; i++)
            {
                positions[i] = (x: p.x, y: p.y, number: i + 1);
                var sum = grid[p.y - 1][p.x - 1] + grid[p.y + 0][p.x - 1] + grid[p.y + 1][p.x - 1] +
                          grid[p.y - 1][p.x + 0] +            0           + grid[p.y + 1][p.x + 0] +
                          grid[p.y - 1][p.x + 1] + grid[p.y + 0][p.x + 1] + grid[p.y + 1][p.x + 1];
                grid[p.y][p.x] = sum>1?sum:1;

                if (sum > input)
                {
                    Console.WriteLine(sum);
                    break;
                }

                p = GetNextPostition(p);
            }
            
            Console.WriteLine(string.Join(Environment.NewLine, grid.OrderByDescending(_=>_.Key).Select(row => string.Join(' ', row.Value.OrderBy(_ => _.Key).Select(_ => $"{_.Value,3}")))));
            /*
            Console.WriteLine(positions[input - 1]);

            Console.WriteLine(Math.Abs(positions[input - 1].x) + Math.Abs(positions[input - 1].y));
            */
            Console.Read();
        }
        public static (int x, int y, int width, Direction d) GetNextPostition((int x, int y, int width, Direction d) current)
        {
            var newPosition = current;

            switch (current.d)
            {
                case Direction.Right:
                    newPosition.x++;
                    break;
                case Direction.Up:
                    newPosition.y++;
                    break;
                case Direction.Left:
                    newPosition.x--;
                    break;
                case Direction.Down:
                    newPosition.y--;
                    break;
            }

            if (((int)current.d % 2 == 0 && Math.Abs(newPosition.x) == current.width) || ((int)current.d % 2 == 1 && Math.Abs(newPosition.y) == current.width))
            {
                newPosition.d = (Direction)(((int)current.d + 1) % 4);
                if (newPosition.d == Direction.Right)
                {
                    newPosition.width++;
                }
            }

            return newPosition;
        }
    }
}
