using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace day7.implementation
{
    public class Item
    {
        private Item(string name)
        {
            Name = name;
        }
        public string Name { get; }

        public int Weight { get; private set; }

        public int CombinedWeight => Weight + Children.Sum(_ => _.CombinedWeight);

        public bool Balanced => Children.GroupBy(_ => _.CombinedWeight).Count() <= 1;

        public Item[] Children { get; private set; } = new Item[0];

        [JsonIgnore]
        public Item Parent { get; private set; }

        public static Dictionary<string, Item> CreatedItems = new Dictionary<string, Item>();

        public static Item Create(string name)
        {
            Item returnValue;
            if (!CreatedItems.TryGetValue(name, out returnValue))
            {
                returnValue = new Item(name);
                CreatedItems.Add(name, returnValue);
            }
            return returnValue;
        }
        public static Item Create(string name, int weight, Item[] children = null)
        {
            Item returnValue;
            if (!CreatedItems.TryGetValue(name, out returnValue))
            {
                returnValue = new Item(name);
                CreatedItems.Add(name, returnValue);
            }
            returnValue.Weight = weight;
            returnValue.Children = children;

            if (children != null)
            {
                foreach (var child in children)
                {
                    child.Parent = returnValue;
                }
            }
            return returnValue;
        }
    }
}
