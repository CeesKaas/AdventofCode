using System;

namespace Day18
{
    public struct Point : IEquatable<Point>
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public readonly int X;
        public readonly int Y;

        public override bool Equals(object obj)
        {
            return obj is Point point && Equals(point);
        }

        public bool Equals(Point other)
        {
            return X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !(left == right);
        }

        internal Point Next(Direction d)
        {
            var x = X;
            var y = Y;
            switch (d)
            {
                case Direction.North:
                    y++;
                    break;
                case Direction.South:
                    y--;
                    break;
                case Direction.East:
                    x++;
                    break;
                case Direction.West:
                    x--;
                    break;
            }
            return new Point(x, y);
        }
        public override string ToString()
        {
            return $"[{X,2},{Y,2}]";
        }
    }
}