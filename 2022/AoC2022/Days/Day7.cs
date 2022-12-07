using Utilities;

namespace AoC2022.Days;

public class Day7
{
    private readonly int Day = int.Parse(nameof(Day7).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    private const int TotalDiskSpace = 70_000_000;
    private const int RequiredFreeDiskSpace = 30_000_000;
    private Directory _rootDirectory = new Directory("/", null);
    public Day7(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        await Parse();
        Console.WriteLine($"Day 7 part 1 answer: {Part1()}");
        Console.WriteLine($"Day 7 part 2 answer: {Part2()}");
    }

    public async Task Parse()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        Directory? currentDir = null;
        bool listMode = false;
        foreach (var line in input)
        {
            if (line.StartsWith("$"))
            {
                //command mode
                listMode = line == "$ ls";
                if (line == "$ cd /")
                {
                    currentDir = _rootDirectory;
                }
                else if (line == "$ cd ..")
                {
                    currentDir = currentDir.Parent;
                }
                else if (line.StartsWith("$ cd"))
                {
                    string destinationDir = line[5..];
                    //change directory
                    currentDir = currentDir.Contents.OfType<Directory>().Single(_ => _.Name == destinationDir);
                }
            }
            else if (listMode)
            {
                if (line.StartsWith("dir"))
                {
                    currentDir?.Contents.Add(new Directory(line.Substring(4), currentDir));
                }
                else
                {
                    var parts = line.Split(" ");
                    var fileName = parts[1];
                    var size = int.Parse(parts[0]);
                    currentDir.Contents.Add(new File(fileName, size, currentDir));
                }
            }
            else
            {
                //do nothing
            }
        }
    }

    public int Part1()
    {
        return Directory.All.Select(_=>_.Size).Where(_=>_<100_000).Sum();
    }

    public int Part2()
    {
        var spaceAvailable = TotalDiskSpace - _rootDirectory.Size;
        var extraSpaceNeeded = RequiredFreeDiskSpace - spaceAvailable;
        return Directory.All.Select(_ => _.Size).Where(_ => _ > extraSpaceNeeded).Min();
    }
}

abstract class DirectoryContents
{
    public string Name { get; }
    public abstract int Size { get; }
    public Directory? Parent { get; }

    protected DirectoryContents(string name, Directory? parent)
    {
        Name = name;
        Parent = parent;
    }
}

class Directory : DirectoryContents
{
    public static List<Directory> All { get; } = new();
    public List<DirectoryContents> Contents { get; } = new();

    public override int Size => Contents.Sum(_ => _.Size);

    public Directory(string name, Directory? parent) : base(name, parent)
    {
        All.Add(this);
    }
}

class File : DirectoryContents
{
    public File(string name, int size, Directory? parent) : base(name, parent)
    {
        Size = size;
    }

    public override int Size { get; }
}

