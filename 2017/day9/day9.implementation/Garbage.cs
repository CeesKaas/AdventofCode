using System;
using System.Collections.Generic;
using System.Text;

namespace day9.implementation
{
    public class Garbage :IStreamPart
    {
        public StringBuilder Contents { get; } = new StringBuilder();
    }
}
