using System;
using System.Net;
using day2.implemenation;

namespace day2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString;
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.Cookie] = "session=53616c7465645f5f85f63314a0cee11b2b23543970d34a959602f6ceaccc21f28df18de536ed26515ca91c2a5d779f21";
                inputString = webClient.DownloadString("http://adventofcode.com/2017/day/2/input");
            }
            {
                var result = new Checksum(Utils.ExtractSpreadSheetFromString(inputString.Trim()));

                Console.WriteLine(result.Result);
            }
            {
                var result = new Checksum2(Utils.ExtractSpreadSheetFromString(inputString.Trim()));

                Console.WriteLine(result.Result);
            }

            Console.Read();
        }
    }
}
