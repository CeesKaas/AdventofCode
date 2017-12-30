using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace day19.implementation
{
    public class Router
    {
        public static string GetRoute(string[] input)
        {
            StringBuilder route = new StringBuilder();



            Cursor location = new Cursor()
            {
                X = input[0].IndexOf('|'),
                Y = 0,
                Direction = Direction.Down
            };

            while (true)
            {
                PrintMap(input, location);
                switch (input.CharacterAtCursor(location))
                {
                    case '|':
                    case '-':
                        break;

                    case '+':
                        location.UpdateDirection(input);
                        break;
                    case ' ':
                        throw new Exception("error in map");
                    default:
                        route.Append(input[location.Y][location.X]);

                        var characterAtNextLocation = input.CharacterAtLocation(location.GetNextLocation());

                        if (characterAtNextLocation == ' ')
                        {
                            Console.WriteLine(location.StepsTaken);
                            return route.ToString();
                        }
                        break;
                }
                location.Step();
            }

        }

        private static void PrintMap(string[] input, Cursor location)
        {
            Console.SetWindowPosition(0, 0);
            var startPoint = Math.Max(0, location.Y - 30);
            for (int i = startPoint; i < Math.Min(input.Length, 60 + startPoint); i++)
            {
                Console.SetCursorPosition(0, i - startPoint);
                Console.Write(input[i]);
            }

            Console.BackgroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(location.X, location.Y - startPoint);
            Console.WriteLine(input[location.Y][location.X]);
            Console.ResetColor();
        }
    }
    class Cursor
    {
        public int X;
        public int Y;
        public Direction Direction;
        public int StepsTaken;

        internal void UpdateDirection(string[] map)
        {
            if (Direction.IsVertical())
            {
                if (map[Y][X - 1] == '-' || Char.IsLetter(map[Y][X - 1]))
                {
                    Direction = Direction.Left;
                }
                else
                {
                    Direction = Direction.Right;
                }
            }
            else
            {
                if (map[Y - 1][X] == '|' || Char.IsLetter(map[Y - 1][X]))
                {
                    Direction = Direction.Up;
                }
                else
                {
                    Direction = Direction.Down;
                }
            }
        }

        public (int x, int y) GetNextLocation()
        {
            switch (Direction)
            {
                case Direction.Up:
                    return (x: X, y: Y - 1);
                case Direction.Down:
                    return (x: X, y: Y + 1);
                case Direction.Left:
                    return (x: X - 1, y: Y);
                case Direction.Right:
                    return (x: X + 1, y: Y);
            }
            return (0, 0);
        }

        internal void Step()
        {
            StepsTaken++;
            switch (Direction)
            {
                case Direction.Up:
                    Y--;
                    break;
                case Direction.Down:
                    Y++;
                    break;
                case Direction.Left:
                    X--;
                    break;
                case Direction.Right:
                    X++;
                    break;
            }
        }
    }
    enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    static class DirectionExtension
    {
        public static bool IsVertical(this Direction d)
        {
            return d == Direction.Down || d == Direction.Up;
        }
        public static char CharacterAtCursor(this string[] map, Cursor c)
        {
            return map.CharacterAtLocation((c.X, c.Y));
        }
        public static char CharacterAtLocation(this string[] map, (int x, int y) location)
        {
            return map[location.y][location.x];
        }
    }
}