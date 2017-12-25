using System;
using System.Collections.Generic;
using System.Linq;

namespace day16.implementation
{
    public class Dance
    {
        private readonly int _places;
        private char[] _danceFloor;
        private List<Move> _moves = new List<Move>();

        public string CurrentState => new string(_danceFloor);

        public Dance(int places, string input)
        {
            _danceFloor = new char[places];
            for (int i = 0; i < places; i++)
            {
                _danceFloor[i] = (char)('a' + i);
            }
            var moves = input.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var move in moves)
            {
                _places = places;
                switch (move[0])
                {
                    case 's':
                        var amount = int.Parse(move.Substring(1));
                        _moves.Add(new MoveS(amount));
                        break;
                    case 'x':
                        var placeA = int.Parse(move.Substring(1).Split('/')[0]);
                        var placeB = int.Parse(move.Substring(1).Split('/')[1]);

                        _moves.Add(new MoveX(placeA, placeB));

                        break;
                    case 'p':
                        var charA = move.Substring(1).Split('/')[0][0];
                        var charB = move.Substring(1).Split('/')[1][0];

                        _moves.Add(new MoveP(charA, charB));
                        break;
                }
            }
        }

        public void Execute()
        {
            foreach (var move in _moves)
            {
                move.Execute(ref _danceFloor);
            }
        }
    }
    interface Move
    {
        void Execute(ref char[] input);
    }
    class MoveS : Move
    {
        private readonly int amount;

        public MoveS(int amount)
        {
            this.amount = amount;
        }

        public void Execute(ref char[] input)
        {
            var inputString = new string(input);
            input = $"{inputString.Substring(input.Length-amount)}{inputString.Substring(0,input.Length - amount)}".ToCharArray();
        }
    }
    class MoveX : Move
    {
        private readonly int placeA;
        private readonly int placeB;

        public MoveX(int placeA, int placeB)
        {
            this.placeA = placeA;
            this.placeB = placeB;
        }

        public void Execute(ref char[] input)
        {
            var temp = input[placeA];
            input[placeA] = input[placeB];
            input[placeB] = temp;
        }
    }
    class MoveP : Move
    {
        private readonly char charA;
        private readonly char charB;

        public MoveP(char charA, char charB)
        {
            this.charA = charA;
            this.charB = charB;
        }

        public void Execute(ref char[] input)
        {
            int indexOfA = Array.IndexOf(input, charA);
            int indexOfB = Array.IndexOf(input, charB);
            input[indexOfA] = charB;
            input[indexOfB] = charA;
        }
    }
}
