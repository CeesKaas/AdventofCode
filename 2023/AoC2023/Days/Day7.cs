using Utilities;
using static AoC2023.Days.Day7;

namespace AoC2023.Days;

public class Day7
{
    private readonly int Day = int.Parse(nameof(Day7).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day7(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 7 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 7 part 2 answer: {await Part2()}");
    }

    public async Task<long> Part1()
    {
        var input = (await _inputFetcher.FetchInputAsStrings(Day)).Select(ParseHandAndBid);
        var sorted = input.OrderBy(_ => _.hand).ToList();
        var accumulate = 0l;
        for (var i = 0; i < sorted.Count; i++)
        {
            accumulate += sorted[i].Bid * (i + 1);
        }
        return accumulate;
    }

    public async Task<long> Part2()
    {
        var input = (await _inputFetcher.FetchInputAsStrings(Day)).Select(ParseHandAndBidPart2);
        var sorted = input.OrderBy(_ => _.hand).ToList();
        var accumulate = 0l;
        for (var i = 0; i < sorted.Count; i++)
        {
            accumulate += sorted[i].Bid * (i + 1);
        }
        return accumulate;
    }

    public static (Hand hand, long Bid) ParseHandAndBid(string input)
    {
        var parts = input.Split(" ");
        return (new(parts[0]), long.Parse(parts[1]));
    }
    public static (HandPart2 hand, long Bid) ParseHandAndBidPart2(string input)
    {
        var parts = input.Split(" ");
        return (new(parts[0]), long.Parse(parts[1]));
    }

    public class Hand : IComparable<Hand>
    {
        public Hand(string cards)
        {
            Cards = cards.Select(ExtensionMethods.ToCardValue).ToArray();
            var cardCounts = Cards.GroupBy(_ => _).ToDictionary(_ => _.Key, _ => _.Count());
            HandType = cardCounts.ToHandType();
        }

        public HandType HandType { get; }
        public CardValue[] Cards { get; }

        public int CompareTo(Hand? other)
        {
            if (other == null) return 1;
            var comparedType = HandType.CompareTo(other.HandType);
            if (comparedType == 0)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    var relativeValue = Cards[i].CompareTo(other.Cards[i]);
                    if (relativeValue != 0)
                    {
                        return relativeValue;
                    }
                }
            }
            return comparedType;
        }
    }
    public class HandPart2 : IComparable<HandPart2>
    {
        public HandPart2(string cards)
        {
            Cards = cards.Select(ExtensionMethods.ToCardValuePart2).ToArray();
            var cardCounts = Cards.GroupBy(_ => _).ToDictionary(_ => _.Key, _ => _.Count());
            HandType = cardCounts.ToHandType();
        }

        public HandType HandType { get; }
        public CardValuePart2[] Cards { get; }

        public int CompareTo(HandPart2? other)
        {
            if (other == null) return 1;
            var comparedType = HandType.CompareTo(other.HandType);
            if (comparedType == 0)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    var relativeValue = Cards[i].CompareTo(other.Cards[i]);
                    if (relativeValue != 0)
                    {
                        return relativeValue;
                    }
                }
            }
            return comparedType;
        }
    }
    public enum HandType : byte
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
    public enum CardValue : byte
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    public enum CardValuePart2 : byte
    {
        Joker,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Queen,
        King,
        Ace
    }
}

public static class ExtensionMethods
{
    public static Day7.HandType ToHandType(this Dictionary<CardValue, int> cardCounts)
    {
        var orderedCardCounts = cardCounts.Select(_ => _.Value).OrderDescending().ToArray();

        if (orderedCardCounts[0] == 5)
        {
            return Day7.HandType.FiveOfAKind;
        }
        if (orderedCardCounts[0] == 4)
        {
            return Day7.HandType.FourOfAKind;
        }
        if (orderedCardCounts[0] == 3)
        {
            if (orderedCardCounts[1] == 2)
            {
                return Day7.HandType.FullHouse;
            }
            else
            {
                return Day7.HandType.ThreeOfAKind;
            }
        }
        if (orderedCardCounts[0] == 2)
        {
            if (orderedCardCounts[1] == 2)
            {
                return Day7.HandType.TwoPair;
            }
            return Day7.HandType.OnePair;
        }
        return Day7.HandType.HighCard;
    }
    public static Day7.CardValue ToCardValue(this char c)
    {
        return c switch
        {
            '2' => Day7.CardValue.Two,
            '3' => Day7.CardValue.Three,
            '4' => Day7.CardValue.Four,
            '5' => Day7.CardValue.Five,
            '6' => Day7.CardValue.Six,
            '7' => Day7.CardValue.Seven,
            '8' => Day7.CardValue.Eight,
            '9' => Day7.CardValue.Nine,
            'T' => Day7.CardValue.Ten,
            'J' => Day7.CardValue.Jack,
            'Q' => Day7.CardValue.Queen,
            'K' => Day7.CardValue.King,
            'A' => Day7.CardValue.Ace,
            _ => throw new NotImplementedException($"no card value for {c} has been set")
        };
    }
    public static Day7.HandType ToHandType(this Dictionary<CardValuePart2, int> cardCounts)
    {
        var jokerCount = cardCounts.TryGetValue(CardValuePart2.Joker, out var count) ? count : 0;
        if (jokerCount == 5) return HandType.FiveOfAKind;

        var orderedCardCounts = cardCounts.Where(_=>_.Key != CardValuePart2.Joker).Select(_ => _.Value).OrderDescending().ToArray();
        orderedCardCounts[0] += jokerCount;

        if (orderedCardCounts[0] == 5)
        {
            return Day7.HandType.FiveOfAKind;
        }
        if (orderedCardCounts[0] == 4)
        {
            return Day7.HandType.FourOfAKind;
        }
        if (orderedCardCounts[0] == 3)
        {
            if (orderedCardCounts[1] == 2)
            {
                return Day7.HandType.FullHouse;
            }
            else
            {
                return Day7.HandType.ThreeOfAKind;
            }
        }
        if (orderedCardCounts[0] == 2)
        {
            if (orderedCardCounts[1] == 2)
            {
                return Day7.HandType.TwoPair;
            }
            return Day7.HandType.OnePair;
        }
        return Day7.HandType.HighCard;
    }
    public static Day7.CardValuePart2 ToCardValuePart2(this char c)
    {
        return c switch
        {
            'J' => Day7.CardValuePart2.Joker,
            '2' => Day7.CardValuePart2.Two,
            '3' => Day7.CardValuePart2.Three,
            '4' => Day7.CardValuePart2.Four,
            '5' => Day7.CardValuePart2.Five,
            '6' => Day7.CardValuePart2.Six,
            '7' => Day7.CardValuePart2.Seven,
            '8' => Day7.CardValuePart2.Eight,
            '9' => Day7.CardValuePart2.Nine,
            'T' => Day7.CardValuePart2.Ten,
            'Q' => Day7.CardValuePart2.Queen,
            'K' => Day7.CardValuePart2.King,
            'A' => Day7.CardValuePart2.Ace,
            _ => throw new NotImplementedException($"no card value for {c} has been set")
        };
    }
}
