using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace day9.implementation
{
    public class StreamParser
    {
        public static (int score, int removedGarbage) ScoreStream(string input)
        {
            Group group = null;
            Group baseGroup = null;
            Garbage garbage = new Garbage();
            bool inGarbage = false;
            bool characterIsCanceled = false;

            foreach (char c in input)
            {
                if (characterIsCanceled)
                {
                    characterIsCanceled = false;
                    continue;
                }
                else
                {
                    if (c == '!')
                    {
                        characterIsCanceled = true;
                        continue;
                    }
                }
                if (inGarbage)
                {
                }

                if (inGarbage)
                {
                    if (c == '>')
                    {
                        inGarbage = false;
                    }
                    else
                    {
                        garbage.Contents.Append(c);
                    }
                    continue;
                }

                switch (c)
                {
                    case '{':
                        var nextGroup = new Group(group);
                        if (nextGroup.Parent == null)
                        {
                            baseGroup = nextGroup;
                            group = nextGroup;
                            continue;
                        }
                        group.Contents.Add(nextGroup);
                        group = nextGroup;
                        break;
                    case '}':
                        group = group.Parent;
                        break;
                    case '<':
                        inGarbage = true;
                        break;
                    case '>':
                        garbage = null;
                        inGarbage = false;
                        break;
                }
            }
            return (score: RecursiveSum(baseGroup ?? new Group(null)), removedGarbage: garbage.Contents.Length);
        }
        public static int RecursiveSum(Group group)
        {
            return group.Score + group.Contents.OfType<Group>()?.Sum(RecursiveSum) ?? 0;
        }
    }
}
