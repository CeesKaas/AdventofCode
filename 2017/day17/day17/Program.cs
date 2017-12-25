using day17.implementation;
using System;

namespace day17
{
    class Program
    {
        static void Main(string[] args)
        {
            const int stepSize = 376;
            var list = new SpinLocker(stepSize);
            list.BuildList(2017);
            var index = list.result.IndexOf(2017);
            Console.WriteLine(list.result[index + 1].ToString());
            Console.WriteLine(list.result[1].ToString());

            int positionOfZero = 0;
            int valueAfterZero = 1;
            int lastIndex = 1;
            for (int i = 1; i < 50_000_000; i++)
            {
                lastIndex = (lastIndex + stepSize) % i + 1;
                if (lastIndex==positionOfZero+1)
                {
                    valueAfterZero = i;
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine(valueAfterZero);

            Console.Read();
        }
    }
}
