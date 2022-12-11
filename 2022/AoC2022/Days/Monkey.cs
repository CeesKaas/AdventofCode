using System.Collections.Immutable;

namespace AoC2022.Days
{
    internal class Monkey
    {
        /*
                    111111111122222222223333
          0123456789012345678901234567890123
        0 Monkey 0:
        1  Starting items: 79, 98
        2  Operation: new = old * 19
        3  Test: divisible by 23
        4    If true: throw to monkey 2
        5    If false: throw to monkey 3 

         */
        public Monkey(string input, int index)
        {
            var lines = input.Split('\n');
            Items = lines[1].Split(":")[1].Split(",", StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
            int? opParameter = null;
            if (int.TryParse(lines[2][24..], out var opParameterValue))
            {
                opParameter = opParameterValue;
            }
            OpString = lines[2][23];
            Operation = lines[2][23] switch
            {
                '*' => old => old * (opParameter ?? old),
                '+' => old => old + (opParameter ?? old),
                _ => throw new NotImplementedException($"operation {lines[2][23]} is not implemented")
            };

            ConditionParameter = int.Parse(lines[3][20..]);
            DestinationIfTrue = int.Parse(lines[4][28..]);
            DestinationIfFalse = int.Parse(lines[5][29..]);
        }

        public List<long> Items { get; }
        public char OpString { get; }
        public Func<long, long> Operation { get; }
        public int ConditionParameter { get; }
        public int DestinationIfTrue { get; }
        public int DestinationIfFalse { get; }
        public long InvestigatedItems { get; private set; }

        public void PlayRoundPart1(List<Monkey> monkeys)
        {
            foreach (var item in Items)
            {
                var newValue = Operation(item) / 3;
                var destination = newValue % ConditionParameter == 0 ? DestinationIfTrue : DestinationIfFalse;
                monkeys[destination].Items.Add(newValue);
                InvestigatedItems++;
            }
            Items.Clear();
        }

        public void PlayRoundPart2(List<Monkey> monkeys)
        {
            var monkeyMultiplier = monkeys.Aggregate(1, (prev, m) => prev * m.ConditionParameter);
            foreach (var item in Items)
            {
                var newValue = Operation(item);
                var destination = newValue % ConditionParameter == 0 ? DestinationIfTrue : DestinationIfFalse;
                monkeys[destination].Items.Add(newValue % monkeyMultiplier);
                InvestigatedItems++;
            }
            Items.Clear();
        }
    }
}