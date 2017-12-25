using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace day18.implementation
{
    public class SoundProgram
    {
        public BigInteger SentNumbers => _registers["SentNumbers"];
        private List<Instruction> _instructions = new List<Instruction>();
        private Registers _registers = new Registers();
        public Queue<BigInteger> OutputQueue { get; } = new Queue<BigInteger>();

        public ManualResetEventSlim WaitingForInput { get; } = new ManualResetEventSlim();
        public Registers Registers => _registers;

        public static SoundProgram Parse(int programId, string input)
        {
            var result = new SoundProgram();
            var lines = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                result._instructions.Add(Instruction.Parse(line, result.OutputQueue, result.WaitingForInput));
            }
            result._registers["p"] = programId;
            result._registers["programId"] = programId;
            return result;
        }

        public void Execute(Queue<BigInteger> inputQueue, ManualResetEventSlim othersWaitingForInput)
        {
            for (int i = 0; i < _instructions.Count;)
            {
                _instructions[i].Execute(ref i, _registers, inputQueue, othersWaitingForInput);
                //Console.WriteLine($"{_registers["programId"]}: {i}, {_instructions[i].GetType()}");
            }
        }
    }
    public abstract class Instruction
    {
        public static Instruction Parse(string instruction, Queue<BigInteger> outputQueue, ManualResetEventSlim WaitingForInput)
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
                case "snd": return new Send(parts[1], outputQueue);
                //rcv a
                case "rcv": return new Receive(parts[1], outputQueue, WaitingForInput);
                //jgz a -2
                case "jgz": return new JumpIfGreaterThenZero(parts[1], parts[2]);
            }
            return null;
        }

        public virtual void Execute(ref int instructionPointer, Registers registers, Queue<BigInteger> inputQueue, ManualResetEventSlim othersWaitingForInput)
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

        public override void Execute(ref int instructionPointer, Registers registers, Queue<BigInteger> inputQueue, ManualResetEventSlim othersWaitingForInput)
        {
            if (int.TryParse(Value, out int value))
            {
                registers[Register] = value;
            }
            else
            {
                registers[Register] = registers[Value];
            }
            base.Execute(ref instructionPointer, registers, inputQueue, othersWaitingForInput);
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
        public override void Execute(ref int instructionPointer, Registers registers, Queue<BigInteger> inputQueue, ManualResetEventSlim othersWaitingForInput)
        {
            if (int.TryParse(Value, out int value))
            {
                registers[Register] += value;
            }
            else
            {
                registers[Register] += registers[Value];
            }
            base.Execute(ref instructionPointer, registers, inputQueue, othersWaitingForInput);
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
        public override void Execute(ref int instructionPointer, Registers registers, Queue<BigInteger> inputQueue, ManualResetEventSlim othersWaitingForInput)
        {
            if (int.TryParse(Value, out int value))
            {
                registers[Register] *= value;
            }
            else
            {
                registers[Register] *= registers[Value];
            }
            base.Execute(ref instructionPointer, registers, inputQueue, othersWaitingForInput);
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
        public override void Execute(ref int instructionPointer, Registers registers, Queue<BigInteger> inputQueue, ManualResetEventSlim othersWaitingForInput)
        {
            if (int.TryParse(Value, out int value))
            {
                registers[Register] = registers[Register] % value;
            }
            else
            {
                registers[Register] = registers[Register] % registers[Value];
            }
            base.Execute(ref instructionPointer, registers, inputQueue, othersWaitingForInput);
        }
    }
    public class Send : Instruction
    {
        public string Register { get; }
        public Queue<BigInteger> DestinationQueue { get; }

        public Send(string register, Queue<BigInteger> destinationQueue)
        {
            Register = register;
            DestinationQueue = destinationQueue;
        }
        public override void Execute(ref int instructionPointer, Registers registers, Queue<BigInteger> inputQueue, ManualResetEventSlim othersWaitingForInput)
        {
            registers["SentNumbers"]++;
            DestinationQueue.Enqueue(registers[Register]);
            base.Execute(ref instructionPointer, registers, inputQueue, othersWaitingForInput);
        }
    }
    public class Receive : Instruction
    {
        public Receive(string registerToStoreRetrievedValueIn, Queue<BigInteger> destinationQueue, ManualResetEventSlim waitingForInput)
        {
            RegisterToStoreRetrievedValueIn = registerToStoreRetrievedValueIn;
            _destinationQueue = destinationQueue;
            _waitingForInput = waitingForInput;
        }

        public string RegisterToStoreRetrievedValueIn { get; }

        private readonly Queue<BigInteger> _destinationQueue;
        private ManualResetEventSlim _waitingForInput;
        public override void Execute(ref int instructionPointer, Registers registers, Queue<BigInteger> inputQueue, ManualResetEventSlim othersWaitingForInput)
        {
            registers["ReceivedNumbers"]++;
            int tries = 100;
            while (--tries>0)
            {
                try
                {
                    BigInteger value = inputQueue.Dequeue();
                    _waitingForInput.Reset();
                    registers[RegisterToStoreRetrievedValueIn] = value;
                    base.Execute(ref instructionPointer, registers, inputQueue, othersWaitingForInput);
                    return;
                }
                catch (InvalidOperationException)
                {
                    _waitingForInput.Set();
                    Thread.Sleep(100);
                    /*if (othersWaitingForInput.IsSet && _destinationQueue.Count == 0)
                    {
                        Console.WriteLine("deadlock");
                        instructionPointer = int.MaxValue;
                        return;
                    }*/
                }
            }
            Console.WriteLine("deadlock");
            instructionPointer = int.MaxValue;
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

        public override void Execute(ref int instructionPointer, Registers registers, Queue<BigInteger> Queue, ManualResetEventSlim othersWaitingForInput)
        {
            if (ShouldExecute(registers))
            {
                if (int.TryParse(AmountToJump, out int value))
                {
                    instructionPointer += value;
                }
                else
                {
                    instructionPointer += (int)registers[AmountToJump];
                }
            }
            else
            {
                instructionPointer++;
            }
        }

        private bool ShouldExecute(Registers registers)
        {
            if (int.TryParse(RegisterToCheck, out var fixedValue))
            {
                return fixedValue > 0;
            }
            return registers[RegisterToCheck] > 0;
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
