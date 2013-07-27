using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostBlade
{
    public class Scrambler : NotifyPropertyChanged
    {
        int _length;
        public int Length
        {
            get { return _length; }
            set
            {
                if (value != _length)
                {
                    if (value <= 0 || value > 50)
                        throw new Exception("Length must be between 0 and 50");
                    _length = value;
                    OnPropertyChanged("Length");
                }
            }
        }

        string _current;
        public string Current
        {
            get { return _current; }
            private set
            {
                if (value != _current)
                {
                    Last = _current;
                    _current = value;
                    OnPropertyChanged("Current");
                }
            }
        }

        string _last;
        public string Last
        {
            get { return _last; }
            private set
            {
                if (value != _last)
                {
                    _last = value;
                    OnPropertyChanged("Last");
                }
            }
        }

        Command _scrambleCommand;
        public Command ScrambleCommand
        {
            get
            {
                if (_scrambleCommand == null)
                    _scrambleCommand = new Command(x => Scramble());
                return _scrambleCommand;
            }
        }

        public Scrambler(int length)
        {
            Length = length;
            Scramble();
        }

        public void Scramble()
        {
            var moves = new List<Move>();
            var random = new Random();
            for (int i = 0; i < Length; i++)
            {
                Move move;
                bool isMoveValid;
                do
                {
                    move = MoveHelpers.GetRandom(random);
                    isMoveValid = true;
                    for (int j = moves.Count - 1; j >= 0; j--)
                    {
                        var m = moves[j];
                        if (MoveHelpers.AreAdjacent(move, m))
                        {
                            break;
                        }
                        else if (MoveHelpers.AreOnSameFace(move, m))
                        {
                            isMoveValid = false;
                            break;
                        }
                    }
                } while (!isMoveValid);
                moves.Add(move);
            }

            var scramble = "";
            for (int i = 0; i < moves.Count; i++)
            {
                scramble += MoveHelpers.GetNotation(moves[i]);
                if (i != moves.Count - 1)
                    scramble += " ";
            }
            Current = scramble;
        }
    }
}
