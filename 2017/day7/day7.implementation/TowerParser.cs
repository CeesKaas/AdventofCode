using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day7.implementation
{
    public class TowerParser
    {
        public static Item[] Parse(string input)
        {
            Regex parser = new Regex(@"(?<Name>.*) \((?<Weight>[0-9]*)\)(?: -> (?:(?<Child>[^, \r\n]*)(?:, )?)*)?");
            var matches = parser.Matches(input);

            var results = new List<Item>();
            foreach (var match in matches.Cast<Match>())
            {
                var name = match.Groups["Name"].Value;
                var weight = int.Parse(match.Groups["Weight"].Value);
                var children = new List<Item>();
                foreach (var child in match.Groups["Child"].Captures.Cast<Capture>())
                {
                    if (!string.IsNullOrEmpty(child.Value))
                    {
                        children.Add(Item.Create(child.Value));
                    }
                }
                results.Add(Item.Create(name, weight, children.ToArray()));
            }

            return results.Where(_ => _.Parent == null).ToArray();
        }
    }
}
