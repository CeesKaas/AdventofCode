using System;

namespace day13.implementation
{
    internal class Scanner
    {
        private readonly int _layerNumber;
        private Layer _layer;
        private int _currentDepth = 0;
        private Direction _direction = Direction.Up;

        public Scanner(int layerNumber, Layer layer)
        {
            _layerNumber = layerNumber;
            _layer = layer;
        }

        public void Scan()
        {
            //Console.WriteLine($"scanner for layer {_layerNumber} currently at {_currentDepth} going {_direction}");
            if (_layer.Packets[_currentDepth] != null)
            {
                //Console.WriteLine($"hit layer {_layerNumber}");
                _layer.Packets[_currentDepth].RiskScore += _layerNumber * _layer.Depth;
                _layer.Packets[_currentDepth].Hit++;
            }
        }

        public void Advance()
        {
            switch (_direction)
            {
                case Direction.Up:
                    _currentDepth++;
                    if (_currentDepth == _layer.Depth-1)
                    {
                        _direction = Direction.Down;
                    }
                    break;
                case Direction.Down:
                    _currentDepth--;
                    if (_currentDepth == 0)
                    {
                        _direction = Direction.Up;
                    }
                    break;
            }
        }
    }

    enum Direction
    {
        Up,
        Down
    }
}