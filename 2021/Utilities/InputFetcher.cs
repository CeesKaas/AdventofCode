using System.Net;
using System.Text;

namespace Utilities
{
    public class InputFetcher : IInputFetcher
    {
        public byte[] FetchInput(int day)
        {
            string cacheFileName = $"day{day}.input";
            if (File.Exists(cacheFileName))
            {
                return File.ReadAllBytes(cacheFileName);
            }
            var request = WebRequest.CreateHttp($"https://adventofcode.com/2021/day/{day}/input");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("session", "53616c7465645f5ff07e1ec0876878ce472c01941dd1ce03a67e86082314424343929c72c09516efef4d5dcc36ee45ce", "/", "adventofcode.com"));
            var response = request.GetResponse();
            using var responseStream = response.GetResponseStream();
            using var reader = new BinaryReader(responseStream);
            var bytes = new byte[response.ContentLength];
            var input = reader.Read(bytes);
            File.WriteAllBytes(cacheFileName, bytes[0..input]);

            return bytes[0..input].ToArray();
        }

        public string FetchInputAsString(int day)
        {
            return Encoding.UTF8.GetString(FetchInput(day));
        }

        public string[] FetchInputAsStrings(int day, char[]? split = null)
        {
            split ??= new char[] { '\r', '\n' };
            return FetchInputAsString(day).Split(split, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }

        public ICollection<T> GetTransformedSplitInputForDay<T>(int day, char[] split, Func<string, T> transformation)
        {
            return FetchInputAsStrings(day, split).Select(transformation).ToList();
        }
        public ICollection<T> GetTransformedSplitInputForDay<T>(int day, Func<string, T> transformation)
        {
            return FetchInputAsStrings(day).Select(transformation).ToList();
        }
    }
}
