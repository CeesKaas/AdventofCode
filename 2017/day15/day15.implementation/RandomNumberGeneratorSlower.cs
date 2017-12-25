using System;

namespace day15.implementation
{
    public class RandomNumberGeneratorSlower
    {
        private int _last;
        private readonly int _gateValue;
        private int _factor;
        public RandomNumberGeneratorSlower(int factor, int initialValue, int gateValue)
        {
            _factor = factor;
            _last = initialValue;
            _gateValue = gateValue;
        }
        public int Next()
        {
            _last = (int)(((uint)_last * _factor) % 2147483647);
            while (_last % _gateValue > 0)
            {
                Next();
            }
            return _last;
        }
    }
}
