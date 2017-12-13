using day7.implementation;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    internal class ItemComparer : IEqualityComparer<Item>
    {
        public bool Equals(Item x, Item y)
        {
            if (x.Name != y.Name ||
                x.Weight != y.Weight)
                return false;
            var xChildren = x.Children.OrderBy(_ => _.Name).ToList();
            var yChildren = y.Children.OrderBy(_ => _.Name).ToList();

            if (xChildren.Count != yChildren.Count)
                return false;

            for (int i = 0; i < xChildren.Count; i++)
            {
                if (!Equals(xChildren[i], yChildren[i]))
                    return false;
            }
            return true;
        }

        public int GetHashCode(Item obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}