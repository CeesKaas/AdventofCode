using System;
using System.IO;
using System.Linq;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Split(Environment.NewLine);

            var validPhrases = 0;
            var complexValidPhrases = 0;

            foreach (var line in input)
            {
                var words = line.Split(' ');
                if (words.Distinct().Count() == words.Length)
                {
                    validPhrases++;
                }
                var sortedWords = words.Select(_ => new string(_.OrderBy(c => c).ToArray()));
                if (sortedWords.Distinct().Count() == words.Length)
                {
                    complexValidPhrases++;
                }
            }
            Console.WriteLine(validPhrases);
            Console.WriteLine(complexValidPhrases);
            Console.Read();
        }
    }
}
