using System;
using System.Net;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            var webClient = new WebClient();
            webClient.Headers[HttpRequestHeader.Cookie] =
                "session=53616c7465645f5f8df88f8841af58accfa0fb53a5305000d39ff2ed128319921b190736a56a39623d6472992c2a2764";
            var inputString = webClient.DownloadString("http://adventofcode.com/2017/day/5/input");
            var steps = ExtractArrayFromString(inputString);

            int i = 0;
            int stepsTaken = 0;
            while (i < steps.Length)
            {
                stepsTaken++;
                var skip = steps[i];
                steps[i] += steps[i]<3?1:-1;
                i += skip;
            }
            Console.WriteLine(stepsTaken);
            Console.Read();
        }
        public static int[] ExtractArrayFromString(string inputString)
        {
            string[] items = inputString.Split(new[] { '\t', ' ','\r','\n' }, StringSplitOptions.RemoveEmptyEntries);
            int[] input = new int[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                input[i] = int.Parse(items[i]);
            }
            return input;
        }
    }
}
