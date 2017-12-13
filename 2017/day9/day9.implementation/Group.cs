using System;
using System.Collections.Generic;

namespace day9.implementation
{
    public class Group : IStreamPart
    {
        public Group(Group parent)
        {
            Score = (parent?.Score??0) + 1;
            Parent = parent;
        }
        public List<IStreamPart> Contents { get; } = new List<IStreamPart>();
        public int Score { get; }
        public Group Parent { get; }
    }
}
