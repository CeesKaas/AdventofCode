using System.Net;
using System.Text;

namespace Utilities;

public class InputFetcher : IInputFetcher
{
    public virtual async Task<string> FetchInputAsString(int day)
    {
        const string cachePath = @"inputs";
        const string marker = "2023";
        var currentLocation = Directory.GetCurrentDirectory();
        var baseDirectory = currentLocation.Substring(0,currentLocation.IndexOf(marker)+marker.Length);
        string cacheFileName = Path.Combine(baseDirectory, cachePath, $"day{day}.input");
        if (File.Exists(cacheFileName))
        {
            return await File.ReadAllTextAsync(cacheFileName);
        }
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Cookie.ToString(), "session=53616c7465645f5fbea062b6d3df21eb6078691c1465f8a4b1781b9f94ac30ce7c03500933be6d8b2546dc90eadd540aef4db492a3cf4eafab17a17f2977acf9");
        var input = await httpClient.GetStringAsync($"https://adventofcode.com/2023/day/{day}/input");
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
