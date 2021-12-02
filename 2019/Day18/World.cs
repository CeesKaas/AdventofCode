using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Day18
{
    public class World
    {
        private bool _cheapClone = true;
        private Dictionary<Point, char> _worldMap;
        private Point _previousLocation;
        private ImmutableList<char> _foundKeys;
        public ImmutableList<Point> TouchedLocations;
        public static ConcurrentDictionary<BigInteger, int> SeenWorlds = new ConcurrentDictionary<BigInteger, int>();
        private Direction _heading;

        public string CurrentState => Stringify(_worldMap, Location);

        public Point Location { get; private set; }

        private World() { }
        public World(Dictionary<Point, char> overview, Point location)
        {
            _worldMap = overview;
            Location = location;
            _previousLocation = new Point(int.MaxValue, int.MaxValue);
            _foundKeys = ImmutableList<char>.Empty;
            TouchedLocations = ImmutableList<Point>.Empty;
        }
        public World CloneAt(Point location, Direction heading)
        {
            var nextWorld = CloneCheapAt(location, heading);
            nextWorld.FixCheapClone();
            return nextWorld;
        }
        public World CloneCheapAt(Point location, Direction heading)
        {
            var nextWorld = new World
            {
                _worldMap = _worldMap,
                Location = location,
                _previousLocation = Location,
                _foundKeys = _foundKeys,
                _heading = heading
            };
            nextWorld.TouchedLocations = TouchedLocations.Add(Location);
            return nextWorld;
        }
        private void FixCheapClone()
        {
            if (_cheapClone)
            {
                _worldMap = _worldMap.ToDictionary(_ => _.Key, _ => _.Value);
                _cheapClone = false;
            }
        }

        public IEnumerable<(Point location, char c, Direction heading)> GetNeighbors(Point p)
        {
            (Direction heading, Point location)[] neighbors = new[] { (Direction.North, p.Next(Direction.North)), (Direction.South, p.Next(Direction.South)), (Direction.East, p.Next(Direction.East)), (Direction.West, p.Next(Direction.West)) };

            foreach (var neighbor in neighbors)
            {
                if (_worldMap.TryGetValue(neighbor.location, out char result) && result != '#')
                {
                    yield return (neighbor.location, result, neighbor.heading);
                }
            }
        }

        private HashAlgorithm _hasher = HashAlgorithm.Create(nameof(SHA1));
        public List<World> Step()
        {
            if (_worldMap.Values.All(_ => _ == '#' || _ == '.'))
            {
                return null;
            }
            var thisWorld = StringifyCurrent();
            var worldHash = new BigInteger(_hasher.ComputeHash(Encoding.ASCII.GetBytes(thisWorld)));
            //very simple loop detection
            if (SeenWorlds.TryGetValue(worldHash,out var stepsTakenToGetHere))
            {
                if (stepsTakenToGetHere > StepsTaken)
                {
                    //found a shorter path to get here so let's let this one continue as well
                    Console.WriteLine($"we already saw this exact scenario but not killed since the current path is shorter then the other one we found");
                }
                else
                {
                    //Console.WriteLine($"killed because we already saw this exact scenario");
                    //Console.WriteLine(thisWorld);
                    return new List<World>();
                }
            }
            SeenWorlds.AddOrUpdate(worldHash, StepsTaken, (key, stepsTakenToGetHere) => stepsTakenToGetHere > StepsTaken ? StepsTaken : stepsTakenToGetHere);

            if (char.IsUpper(_worldMap[Location]))
            {
                if (_foundKeys.Contains(char.ToLower(_worldMap[Location])))
                {
                    FixCheapClone();
                    //We've got the key so we can open the door and move on
                    _worldMap[Location] = '.';
                }
                else
                {
                    //We're at a door that we can't open so no more steps to be taken
                    return new List<World>();
                }
            }

            List<World> worldsToReturn;
            if (char.IsLower(_worldMap[Location]))
            {
                FixCheapClone();
                //We're at a key so lets pick it up and split of into all possible directions (including where we came from)
                _foundKeys = _foundKeys.Add(_worldMap[Location]);
                _worldMap[Location] = '.';
                //We've got all keys and been through all doors so we must be done.
                if (_worldMap.Values.All(_ => _ == '#' || _ == '.'))
                {
                    return null;
                }
                //Console.Clear();
                //Console.WriteLine("Found Key:");
                //Console.WriteLine(thisWorld);
                worldsToReturn = GetNeighbors(Location).Select(_ => CloneAt(_.location, _.heading)).ToList();
            }
            else
            {
                // just a normal location so return worlds where we've moved to new locations
                worldsToReturn = GetNeighbors(Location).Where(_ => _.location != _previousLocation).Select(_ => CloneCheapAt(_.location, _.heading)).ToList();
            }
            //this makes it kinda depth first search
            if (worldsToReturn.Count == 1)
            {
                List<World> list = worldsToReturn.Single().Step();
                return list ?? worldsToReturn; //results in double stepping the last world but that shouldn't matter much
            }
            return worldsToReturn;
        }
        public int StepsTaken => TouchedLocations.Count;

        public static string Stringify(Dictionary<Point, char> world, params Point[] currentLocations)
        {
            return string.Join("\r\n", world.GroupBy(item => item.Key.Y).Select(group => group.OrderBy(groupItem => groupItem.Key.X).Select(groupItem => currentLocations.Contains(groupItem.Key) ? '@' : groupItem.Value)).Select(line => new string(line.ToArray())));
        }
        public string StringifyCurrent()
        {
            return $"{string.Join("\r\n", _worldMap.GroupBy(item => item.Key.Y).Select(group => group.OrderBy(groupItem => groupItem.Key.X).Select(groupItem => groupItem.Key == Location ? GetHeadingCharacter(_heading) : groupItem.Value)).Select(line => new string(line.ToArray())))}\r\nKeys:{string.Join(",", _foundKeys.OrderBy(_ => _))}";
        }

        private char GetHeadingCharacter(Direction heading)
        {
            switch (heading)
            {
                case Direction.North: return '^';
                case Direction.East: return '>';
                case Direction.South: return 'v';
                case Direction.West: return '<';
                default: return '@';
            }
        }
    }
}