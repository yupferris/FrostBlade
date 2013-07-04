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
            var scramble = "";
            var random = new Random();
            Moves? lastMove = null;
            for (int i = 0; i < Length; i++)
            {
                Moves move;
                string moveNotation;
                do
                {
                    move = MoveHelpers.GetRandom(random);
                    moveNotation = MoveHelpers.GetNotation(move);
                } while (lastMove.HasValue && moveNotation.First() == MoveHelpers.GetNotation(lastMove.Value).First());
                scramble += moveNotation;
                if (i != Length - 1)
                    scramble += " ";
                lastMove = move;
            }
            Current = scramble;
        }
    }
}
