using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class BitArrayExtensions
    {
        public static int ToInt(this BitArray ba)
        {
            var intArray = new int[1];
            ba.CopyTo(intArray, 0);
            return intArray[0];
        }
    }
}
