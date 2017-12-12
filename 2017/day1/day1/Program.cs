using System;
using System.Net;
using day1.implementation;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString;
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Cookie] = "session=53616c7465645f5f85f63314a0cee11b2b23543970d34a959602f6ceaccc21f28df18de536ed26515ca91c2a5d779f21";
                inputString = webClient.DownloadString("http://adventofcode.com/2017/day/1/input");
            }
            var input = Summer.ExtractArrayFromString(inputString.Trim());

            var result = Summer.DoWork(input);

            Console.WriteLine(result);

            Console.Read();
        }

    }
}
