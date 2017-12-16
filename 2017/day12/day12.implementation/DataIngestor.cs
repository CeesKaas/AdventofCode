using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace day12.implementation
{
    public class DataIngestor
    {
        public static void IngestData(string data)
        {
            var matches = new Regex(@"(?<Name>.*) <-> (?:(?<Child>[^, \r\n]*)(?:, )?)*").Matches(data);
            var neo4j = Neo4j.Driver.V1.GraphDatabase.Driver("bolt://192.168.178.16:7687").Session();
            foreach (var match in matches.Cast<Match>())
            {
                Console.WriteLine(match.Groups["Name"].Value);
                var program = match.Groups["Name"].Value;
                foreach (var c in match.Groups["Child"].Captures.Cast<Capture>())
                {
                    if (string.IsNullOrEmpty(c.Value))
                    {
                        continue;
                    }
                    //Console.WriteLine("\t" + c.Value);
                    var query = $@"MERGE (a:Program {{Id: {program}}})
MERGE (b:Program {{Id: {c.Value}}})
MERGE (a)-[:PIPE]-(b)";
                    neo4j.Run(query);
                    //Console.WriteLine(query);
                }
            }
            //add ungrouped label to all
            neo4j.Run(@"Match (p)
set p:Ungrouped");
            bool newGroupFound = true;
            long ungroupedId = 0;
            int groupId = 0;
            while (newGroupFound)
            {
                groupId++;
                neo4j.Run($@"MATCH (p:Program)-[*]-(f:Program) WHERE p.Id = {ungroupedId} set f:Group{groupId} set p:Group{groupId} remove f:Ungrouped remove p:Ungrouped");
                var statementResult = neo4j.Run($@"MATCH (p:Ungrouped) return p.Id limit 1").ToList();
                if (statementResult.Any())
                {
                    ungroupedId = (long) statementResult.First().Values.First().Value;
                    newGroupFound = true;
                }
                else
                {
                    newGroupFound = false;
                }
            }
            Console.WriteLine(groupId);
        }
    }
}
