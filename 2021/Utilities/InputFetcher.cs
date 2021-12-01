using System.Net;
using System.Text;

namespace Utilities
{
    public class InputFetcher : IInputFetcher
    {
        public string FetchInputAsString(int day)
        {
            const string relativeCachePath = @"..\..\..\..\inputs\";
            string cacheFileName = Path.Combine(relativeCachePath, $"day{day}.input");
            if (File.Exists(cacheFileName))
            {
                return File.ReadAllText(cacheFileName);
            }
            var request = WebRequest.CreateHttp($"https://adventofcode.com/2021/day/{day}/input");
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("session", "53616c7465645f5ff07e1ec0876878ce472c01941dd1ce03a67e86082314424343929c72c09516efef4d5dcc36ee45ce", "/", "adventofcode.com"));
            var response = request.GetResponse();
            using var responseStream = response.GetResponseStream();
            using var reader = new StreamReader(responseStream, Encoding.UTF8);
            var input = reader.ReadToEnd();
            File.WriteAllText(cacheFileName, input);

            return input;
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
