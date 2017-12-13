using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace day8.implementation
{
    public class CommandParser
    {
        public static (Dictionary<string, Register> result, int max) ParseAndExecuteCommands(string input)
        {
            var commands = new Regex(@"(?<Register>\S*) (?<Operation>inc|dec) (?<Amount>[-0-9]*) if (?<ConditionRegister>\S*) (?<Comparison>[<>!=]*) (?<ComparisonAmount>[-0-9]*)").Matches(input);
            var registers = new Dictionary<string, Register>();
            int max = int.MinValue;
            foreach (var command in commands.Cast<Match>())
            {
                var conditionRegisterName = command.Groups["ConditionRegister"].Value;
                if (!registers.TryGetValue(conditionRegisterName, out Register conditionRegister))
                {
                    conditionRegister = new Register(conditionRegisterName);
                    registers.Add(conditionRegisterName, conditionRegister);
                }

                int comparisonAmount = int.Parse(command.Groups["ComparisonAmount"].Value);
                string comparisonOperator = command.Groups["Comparison"].Value;
                Func<int, int, bool> comparison;
                switch (comparisonOperator)
                {
                    case "==":
                        comparison = (a, b) => a == b;
                        break;
                    case ">=":
                        comparison = (a, b) => a >= b;
                        break;
                    case "<=":
                        comparison = (a, b) => a <= b;
                        break;
                    case "!=":
                        comparison = (a, b) => a != b;
                        break;
                    case ">":
                        comparison = (a, b) => a > b;
                        break;
                    case "<":
                        comparison = (a, b) => a < b;
                        break;
                    default:
                        comparison = (a, b) => throw new Exception();
                        break;
                }

                if (!comparison(conditionRegister.Value, comparisonAmount))
                {
                    continue;
                }

                var registerName = command.Groups["Register"].Value;
                if (!registers.TryGetValue(registerName, out Register register))
                {
                    register = new Register(registerName);
                    registers.Add(registerName, register);
                }

                int amount = int.Parse(command.Groups["Amount"].Value);
                string operation = command.Groups["Operation"].Value;

                switch (operation)
                {
                    case "inc":
                        register.Value += amount;
                        break;
                    case "dec":
                        register.Value -= amount;
                        break;
                }
                max = Math.Max(max, register.Value);
            }
            return (registers,max);
        }
    }

    public class Register
    {
        public string Name { get; }
        public int Value { get; set; }
        public Register(string name) => Name = name;
    }
}
