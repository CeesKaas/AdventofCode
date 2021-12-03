using System.Text.RegularExpressions;
using Utilities;

namespace AoC2020.Days;

public class Day4
{
    private readonly IInputFetcher _inputFetcher;
    public Day4(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 4 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 4 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.FetchInputAsString(4);
        var passportParser = new Regex("(([a-z]{3}):([a-z0-9#]*)\\s*)*", RegexOptions.Singleline);
        var passports = input.Replace("\r\n", "\n").Split("\n\n");
        var requiredFields = new string[] {
            "byr",// (Birth Year)
            "iyr",// (Issue Year)
            "eyr",// (Expiration Year)
            "hgt",// (Height)
            "hcl",// (Hair Color)
            "ecl",// (Eye Color)
            "pid"// (Passport ID)
        };
        var valid = 0;
        foreach (var passport in passports)
        {
            var parsedPassport = passportParser.Match(passport);
            if (requiredFields.All(field => parsedPassport.Groups[2].Captures.Any(cap => cap.Value == field)))
                valid++;
        }

        return valid;
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.FetchInputAsString(4);
        var passportParser = new Regex("(([a-z]{3}):([a-z0-9#]*)\\s*)*", RegexOptions.Singleline);
        var hairColorCheck = new Regex("^#[0-9a-f]{6}$", RegexOptions.Singleline);
        var passportIdCheck = new Regex("^[0-9]{9}$", RegexOptions.Singleline);

        string[] allowedEyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        var passports = input.Replace("\r\n", "\n").Split("\n\n");
        var requiredFields = new string[] {
            "byr",// (Birth Year)
            "iyr",// (Issue Year)
            "eyr",// (Expiration Year)
            "hgt",// (Height)
            "hcl",// (Hair Color)
            "ecl",// (Eye Color)
            "pid"// (Passport ID)
        };
        var valid = 0;
        foreach (var passport in passports)
        {
            var parsedPassport = passportParser.Match(passport);
            var fieldKeys = parsedPassport.Groups[2].Captures.Select(_ => _.Value).ToArray();

            var birthYearFieldIndex = Array.IndexOf(fieldKeys, "byr");
            if (birthYearFieldIndex < 0 || !int.TryParse(parsedPassport.Groups[3].Captures[birthYearFieldIndex].Value, out var birthYear) || birthYear < 1920 || birthYear > 2002)
            {
                Console.WriteLine($"Birth Year invalid in {passport}");
                continue;
            }

            var issueYearFieldIndex = Array.IndexOf(fieldKeys, "iyr");
            if (issueYearFieldIndex < 0 || !int.TryParse(parsedPassport.Groups[3].Captures[issueYearFieldIndex].Value, out var issueYear) || issueYear < 2010 || issueYear > 2020)
            {
                Console.WriteLine($"Issue Year invalid in {passport}");
                continue;
            }

            var expirationYearFieldIndex = Array.IndexOf(fieldKeys, "eyr");
            if (expirationYearFieldIndex < 0 || !int.TryParse(parsedPassport.Groups[3].Captures[expirationYearFieldIndex].Value, out var expirationYear) || expirationYear < 2020 || expirationYear > 2030)
            {
                Console.WriteLine($"Experation Year invalid in {passport}");
                continue;
            }


            var heightFieldIndex = Array.IndexOf(fieldKeys, "hgt");
            if (heightFieldIndex < 0)
            {
                Console.WriteLine($"Height field not foundin {passport}");
                continue;
            }
            var heightString = parsedPassport.Groups[3].Captures[heightFieldIndex].Value;
            if (!int.TryParse(heightString[0..^2], out var height))
            {
                Console.WriteLine($"Height not a number in {passport}");
                continue;
            }
            switch (heightString[^2..^0].ToString())
            {
                case "cm":
                    if (height < 150 || height > 193)
                    {
                        Console.WriteLine($"Height in cm out of range in {passport}");
                        continue;
                    }
                    break;
                case "in":
                    if (height < 59 || height > 76)
                    {
                        Console.WriteLine($"Height in inch out of range in {passport}");
                        continue;
                    }
                    break;
                default:
                    continue;
            }

            var hairColorFieldIndex = Array.IndexOf(fieldKeys, "hcl");
            if (hairColorFieldIndex < 0 || !hairColorCheck.IsMatch(parsedPassport.Groups[3].Captures[hairColorFieldIndex].Value))
            {
                Console.WriteLine($"Hair Color invalid in {passport}");
                continue;
            }

            var eyeColorFieldIndex = Array.IndexOf(fieldKeys, "ecl");
            if (eyeColorFieldIndex < 0 || !allowedEyeColors.Contains(parsedPassport.Groups[3].Captures[eyeColorFieldIndex].Value))
            {
                Console.WriteLine($"Eye Color invalid in {passport}");
                continue;
            }

            var passportIdFieldIndex = Array.IndexOf(fieldKeys, "pid");
            if (passportIdFieldIndex < 0 || !passportIdCheck.IsMatch(parsedPassport.Groups[3].Captures[passportIdFieldIndex].Value))
            {
                Console.WriteLine($"Passport ID invalid in {passport}");
                continue;
            }

            valid++;
        }

        return valid;
    }
}
