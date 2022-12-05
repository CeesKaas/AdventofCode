using Utilities;

namespace AoC2022.Days;

public class Day1
{
    private readonly int Day = int.Parse(nameof(Day1).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day1(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        var totalsPerElf = await Parse();
    
        Console.WriteLine($"Day 1 part 1 answer: {Part1(totalsPerElf)}");
        Console.WriteLine($"Day 1 part 2 answer: {Part2(totalsPerElf)}");
    }
    
    public async Task<Dictionary<string,int>> Parse()
    {
        var input = (await _inputFetcher.FetchInputAsString(Day)).Split('\n');
        Dictionary<string,List<int>> elves = new ();
        List<int> currentElf = new ();
        var elfCounter = 0;
        elves.Add((++elfCounter).ToString(), currentElf);
        
        foreach (var line in input)
        {
            if (!int.TryParse(line, out var value))
            {
                currentElf = new ();
                elves.Add((++elfCounter).ToString(), currentElf);
                Console.WriteLine("Created new elf");
            }
            else
            {
                currentElf.Add(value);
            }
        }
        var totalsPerElf = elves.ToDictionary(_=>_.Key,_=>_.Value.Sum());
        return totalsPerElf;
    }

    public int Part1(Dictionary<string,int> totalsPerElf)
    {        
        var maxElf = totalsPerElf.MaxBy(_=>_.Value);
        
        Console.WriteLine($"Elf with most calories was: {maxElf.Key} with {maxElf.Value}");
        return maxElf.Value;
    }

    public int Part2(Dictionary<string,int> totalsPerElf)
    {
        return totalsPerElf.Values.OrderByDescending(_=>_).Take(3).Sum();
    }
    
    public async Task<int> Part1()
    {
        return Part1(await Parse());
    }
    
    public async Task<int> Part2()
    {
        return Part2(await Parse());
    }
}
