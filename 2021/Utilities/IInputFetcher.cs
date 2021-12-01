namespace Utilities
{
    public interface IInputFetcher
    {
        public byte[] FetchInput(int day);
        public string FetchInputAsString(int day);
        string[] FetchInputAsStrings(int day, char[]? split = null);
        ICollection<T> GetTransformedSplitInputForDay<T>(int day, Func<string, T> transformation);
        ICollection<T> GetTransformedSplitInputForDay<T>(int day, char[] split, Func<string, T> transformation);
    }
}