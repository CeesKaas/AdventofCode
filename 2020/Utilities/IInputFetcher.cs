namespace Utilities;

public interface IInputFetcher
{
    public Task<string> FetchInputAsString(int day);
    Task<string[]> FetchInputAsStrings(int day, char[]? split = null);
    Task<ICollection<T>> GetTransformedSplitInputForDay<T>(int day, Func<string, T> transformation);
    Task<ICollection<T>> GetTransformedSplitInputForDay<T>(int day, char[] split, Func<string, T> transformation);
}
