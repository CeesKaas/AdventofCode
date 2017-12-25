using System;

namespace day15.implementation
{
    public class RandomNumberGenerator
    {
        private int _last;
        private int _factor;
        public RandomNumberGenerator(int factor, int initialValue)
        {
            _factor = factor;
            _last = initialValue;
        }
        public int Next()
        {
            _last = (int)(((uint)_last * _factor) % 2147483647);
            return _last;
        }
    }
}
