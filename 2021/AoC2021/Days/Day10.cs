using Utilities;

namespace AoC2021.Days;

public class Day10
{
    private readonly IInputFetcher _inputFetcher;
    public Day10(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        var answer = await Part1And2();
        Console.WriteLine($"Day 10 part 1 answer: {answer.partOne}");
        Console.WriteLine($"Day 10 part 2 answer: {answer.partTwo}");
    }

    public async Task<(int partOne, long partTwo)> Part1And2()
    {
        var input = await _inputFetcher.FetchInputAsStrings(10);

        var syntaxErrors = 0;

        List<(string line, Stack<char> remainingExpectedChars)> incompleteStrings = new();
        foreach (var line in input)
        {
            var nextExpectedCharacterStack = new Stack<char>();

            try
            {
                foreach (var character in line)
                {
                    if (nextExpectedCharacterStack.Count == 0)
                    {
                        nextExpectedCharacterStack.Push(GetClosingFor(character));
                    }
                    var nextExpectedChar = nextExpectedCharacterStack.Pop();
                    if (character != nextExpectedChar)
                    {
                        if (IsClosing(character))
                        {
                            throw new SyntaxErrorException(character);
                        }
                        nextExpectedCharacterStack.Push(nextExpectedChar);
                        nextExpectedCharacterStack.Push(GetClosingFor(character));
                    }
                }
                if (nextExpectedCharacterStack.Count > 0)
                {
                    throw new IncompleteStringException();
                }
            }
            catch (IncompleteStringException)
            {
                incompleteStrings.Add((line, nextExpectedCharacterStack));
            }
            catch (SyntaxErrorException ex)
            {
                syntaxErrors += GetErrorFor(ex.FailingChar);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine($"{line} has more closing characters then expected");
            }
        }

        var completionScores = new List<long>();

        foreach (var (line, remainingChars) in incompleteStrings)
        {
            long completionScore = 0;
            var completionChars = new List<char>();
            while (remainingChars.Count > 1)
            {
                completionScore *= 5;
                var nextChar = remainingChars.Pop();
                completionScore += nextChar switch
                {
                    ')' => 1,
                    ']' => 2,
                    '}' => 3,
                    '>' => 4,
                    _ => 0
                };
                completionChars.Add(nextChar);
            }
            //Console.WriteLine($"{line} {new string(completionChars.ToArray())} resulted in completion score {completionScore}");
            completionScores.Add(completionScore);
        }

        var orderedCompletionScores = completionScores.OrderBy(_ => _).ToList();
        var middleScore = orderedCompletionScores.Skip(completionScores.Count / 2).First();

        return (syntaxErrors, middleScore);
    }

    private char GetClosingFor(char c) => c switch
    {
        '(' => ')',
        '{' => '}',
        '<' => '>',
        '[' => ']',
        _ => throw new ArgumentOutOfRangeException(nameof(c), $"only accepting ({{[< as input, input was {c} ({(int)c})")
    };

    private bool IsClosing(char c) => c switch
    {
        ')' => true,
        '}' => true,
        '>' => true,
        ']' => true,
        '(' => false,
        '{' => false,
        '<' => false,
        '[' => false,
        _ => throw new ArgumentOutOfRangeException(nameof(c), $"only accepting )}}]> as input, input was {c} ({(int)c})")
    };
    private int GetErrorFor(char c) => c switch
    {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137,
        _ => throw new ArgumentOutOfRangeException(nameof(c), $"only accepting )}}]> as input, input was {c} ({(int)c})")
    };
}

public class IncompleteStringException : Exception
{

}
public class SyntaxErrorException : Exception
{
    public char FailingChar { get; init; }
    public SyntaxErrorException(char failingChar)
    {
        FailingChar = failingChar;
    }
}