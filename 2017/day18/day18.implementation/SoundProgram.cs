using System;
using System.Collections.Generic;
using System.Numerics;

namespace day18.implementation
{
    public class SoundProgram
    {
        public BigInteger LastRecoveredValue => _registers["LastRecoveredSound"];
        private List<Instruction> _instructions = new List<Instruction>();
        private Registers _registers = new Registers();

        public static SoundProgram Parse(string input)
        {
            var result = new SoundProgram();
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                result._instructions.Add(Instruction.Parse(line));
            }
            return result;
        }

        public void Execute()
        {
            for (int i = 0; i < _instructions.Count;)
            {
                _instructions[i].Execute(ref i, _registers);
            }
        }
    }
    public abstract class Instruction
    {
        public static Instruction Parse(string instruction)
        {
            var parts = instruction.Split(' ');
            switch (parts[0])
            {
                //set a 1
                case "set": return new Set(parts[1], parts[2]);
                //add a 2
                case "add": return new Add(parts[1], parts[2]);
                //mul a a
                case "mul": return new Multiply(parts[1], parts[2]);
                //mod a 5
                case "mod": return new Modulus(parts[1], parts[2]);
                //snd a
                case "snd": return new PlaySound(parts[1]);
                //rcv a
                case "rcv": return new RecoverLastPlayedSound(parts[1]);
                //jgz a -2
                case "jgz": return new JumpIfGreaterThenZero(parts[1], parts[2]);
            }
            return null;
        }

        public virtual void Execute(ref int instructionPointer, Registers registers)
        {
            instructionPointer++;
        }
    }

    public class Set : Instruction
    {
        public Set(string register, string value)
        {
            Register = register;
            Value = value;
        }

        public string Register { get; }
        public string Value { get; }

        public override void Execute(ref int instructionPointer, Registers registers)
        {
            if (int.TryParse(Value, out int value))
            {
                registers[Register] = value;
            }
            else
            {
                registers[Register] = registers[Value];
            }
            base.Execute(ref instructionPointer, registers);
        }
    }
    public class Add : Instruction
    {
        public string Register { get; }
        public string Value { get; }

        public Add(string register, string value)
        {
            Register = register;
            Value = value;
        }
        public override void Execute(ref int instructionPointer, Registers registers)
        {
            if (int.TryParse(Value, out int value))
            {
                registers[Register] += value;
            }
            else
            {
                registers[Register] += registers[Value];
            }
            base.Execute(ref instructionPointer, registers);
        }
    }
    public class Multiply : Instruction
    {
        public string Register { get; }
        public string Value { get; }

        public Multiply(string register, string value)
        {
            Register = register;
            Value = value;
        }
        public override void Execute(ref int instructionPointer, Registers registers)
        {
            if (int.TryParse(Value, out int value))
            {
                registers[Register] *= value;
            }
            else
            {
                registers[Register] *= registers[Value];
            }
            base.Execute(ref instructionPointer, registers);
        }
    }
    public class Modulus : Instruction
    {
        public string Register { get; }
        public string Value { get; }

        public Modulus(string register, string value)
        {
            Register = register;
            Value = value;
        }
        public override void Execute(ref int instructionPointer, Registers registers)
        {
            if (int.TryParse(Value, out int value))
            {
                registers[Register] = registers[Register] % value;
            }
            else
            {
                registers[Register] = registers[Register] % registers[Value];
            }
            base.Execute(ref instructionPointer, registers);
        }
    }
    public class PlaySound : Instruction
    {
        public string Register { get; }
        public PlaySound(string register)
        {
            Register = register;
        }
        public override void Execute(ref int instructionPointer, Registers registers)
        {
            registers["LastPlayedSound"] = registers[Register];
            base.Execute(ref instructionPointer, registers);
        }
    }
    public class RecoverLastPlayedSound : Instruction
    {
        public RecoverLastPlayedSound(string registerToCheck)
        {
            RegisterToCheck = registerToCheck;
        }

        public string RegisterToCheck { get; }

        public override void Execute(ref int instructionPointer, Registers registers)
        {
            if (registers[RegisterToCheck] != 0)
            {
                registers["LastRecoveredSound"] = registers["LastPlayedSound"];
                instructionPointer = int.MaxValue;
            }
            else
            {
                base.Execute(ref instructionPointer, registers);
            }
        }
    }
    public class JumpIfGreaterThenZero : Instruction
    {
        public JumpIfGreaterThenZero(string registerToCheck, string amountToJump)
        {
            RegisterToCheck = registerToCheck;
            AmountToJump = amountToJump;
        }

        public string RegisterToCheck { get; }
        public string AmountToJump { get; }

        public override void Execute(ref int instructionPointer, Registers registers)
        {
            if (registers[RegisterToCheck] > 0)
            {
                if (int.TryParse(AmountToJump, out int value))
                {
                    instructionPointer += value;
                }
                else
                {
                    instructionPointer += (int) registers[AmountToJump];
                }
            }
            else
            {
                instructionPointer++;
            }
        }
    }

    public class Registers
    {
        private Dictionary<string, BigInteger> _registers = new Dictionary<string, BigInteger>();
        public BigInteger this[string name]
        {
            get
            {
                BigInteger result;
                if (!_registers.TryGetValue(name, out result))
                {
                    _registers.Add(name, 0);
                }
                return result;
            }
            set
            {
                _registers[name] = value;
            }
        }
    }
}
