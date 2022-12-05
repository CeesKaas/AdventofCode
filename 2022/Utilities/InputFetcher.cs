using System.Net;
using System.Text;

namespace Utilities;

public class InputFetcher : IInputFetcher
{
    public virtual async Task<string> FetchInputAsString(int day)
    {
        const string cachePath = @"inputs";
        const string marker = "2022";
        var currentLocation = Directory.GetCurrentDirectory();
        var baseDirectory = currentLocation.Substring(0,currentLocation.IndexOf(marker)+marker.Length);
        string cacheFileName = Path.Combine(baseDirectory, cachePath, $"day{day}.input");
        if (File.Exists(cacheFileName))
        {
            return await File.ReadAllTextAsync(cacheFileName);
        }
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Cookie.ToString(), "session=53616c7465645f5fd1b5bd5aaa78e059b4286ca0ec77bbbf3f7ab44adaa063b5e79e4c5275dfb7c3bd7425815db1fbc6a0b4eb9991236810ecaaad162fcf4aec");
        var input = await httpClient.GetStringAsync($"https://adventofcode.com/2022/day/{day}/input");
        File.WriteAllText(cacheFileName, input);

        return input;
    }

    public virtual async Task<string[]> FetchInputAsStrings(int day, char[]? split = null)
    {
        split ??= new char[] { '\r', '\n' };
        return (await FetchInputAsString(day)).Split(split, StringSplitOptions.RemoveEmptyEntries).ToArray();
    }

    public virtual async Task<ICollection<T>> GetTransformedSplitInputForDay<T>(int day, char[] split, Func<string, T> transformation)
    {
        return (await FetchInputAsStrings(day, split)).Select(transformation).ToList();
    }
    public virtual async Task<ICollection<T>> GetTransformedSplitInputForDay<T>(int day, Func<string, T> transformation)
    {
        return (await FetchInputAsStrings(day)).Select(transformation).ToList();
    }
}
