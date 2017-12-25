using System;
using System.Collections.Generic;

namespace day17.implementation
{
    public class SpinLocker
    {
        private readonly int _stepSize;
        public List<int> result { get; private set; }
        public SpinLocker(int stepSize)
        {
            _stepSize = stepSize;
        }
        public void BuildList(int steps)
        {
            result = new List<int>();
            result.Add(0);
            var lastIndex = 0;
            for (int i = 1; i <= steps; i++)
            {
                lastIndex = (lastIndex + _stepSize) % i + 1;
                if (lastIndex > i)
                {
                    result.Add(i);
                }
                else
                {
                    result.Insert(lastIndex, i);
                    if (lastIndex==1)
                    {
                        Console.WriteLine(i);
                    }
                }
            }
        }
    }
}
