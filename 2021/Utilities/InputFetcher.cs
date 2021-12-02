using System.Net;
using System.Text;

namespace Utilities;

public class InputFetcher : IInputFetcher
{
    public async Task<string> FetchInputAsString(int day)
    {
        const string relativeCachePath = @"..\..\..\..\inputs\";
        string cacheFileName = Path.Combine(relativeCachePath, $"day{day}.input");
        //if (File.Exists(cacheFileName))
        //{
        //    return await File.ReadAllTextAsync(cacheFileName);
        //}
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add(HttpRequestHeader.Cookie.ToString(), "session=53616c7465645f5ff07e1ec0876878ce472c01941dd1ce03a67e86082314424343929c72c09516efef4d5dcc36ee45ce");
        var input = await httpClient.GetStringAsync($"https://adventofcode.com/2021/day/{day}/input");
        File.WriteAllText(cacheFileName, input);

        return input;
    }

    public async Task<string[]> FetchInputAsStrings(int day, char[]? split = null)
    {
        split ??= new char[] { '\r', '\n' };
        return (await FetchInputAsString(day)).Split(split, StringSplitOptions.RemoveEmptyEntries).ToArray();
    }

    public async Task<ICollection<T>> GetTransformedSplitInputForDay<T>(int day, char[] split, Func<string, T> transformation)
    {
        return (await FetchInputAsStrings(day, split)).Select(transformation).ToList();
    }
    public async Task<ICollection<T>> GetTransformedSplitInputForDay<T>(int day, Func<string, T> transformation)
    {
        return (await FetchInputAsStrings(day)).Select(transformation).ToList();
    }
}
