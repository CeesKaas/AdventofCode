using System.Net;
using System.Text;

namespace Utilities;

public class InputFetcher : IInputFetcher
{
    public virtual async Task<string> FetchInputAsString(int day)
    {
        const string cachePath = @"inputs";
        const string marker = "2024";
        var currentLocation = Directory.GetCurrentDirectory();
        var baseDirectory = currentLocation.Substring(0,currentLocation.IndexOf(marker)+marker.Length);
        string cacheFileName = Path.Combine(baseDirectory, cachePath, $"day{day}.input");
        if (File.Exists(cacheFileName))
        {
            return await File.ReadAllTextAsync(cacheFileName);
        }
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Cookie.ToString(), "session=53616c7465645f5f8d5da9b4a5d5f7941fc3f51c7b72dff3cade38673ca8de5e3bd2ddf75673f84f3781458480ec33c3d01ae136c251d5826856e78217a876e9");
        var input = await httpClient.GetStringAsync($"https://adventofcode.com/2024/day/{day}/input");
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
