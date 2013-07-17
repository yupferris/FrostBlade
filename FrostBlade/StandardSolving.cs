using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrostBlade
{
    public class StandardSolving
    {
        readonly Scrambler _scrambler = new Scrambler(25);
        public Scrambler Scrambler { get { return _scrambler; } }

        readonly Timer _timer = new Timer();
        public Timer Timer { get { return _timer; } }

        readonly History _history = new History();
        public History History { get { return _history; } }

        public StandardSolving()
        {
            Timer.Stopped += (sender, e) =>
            {
                History.Entries.Add(new History.Entry(History, Timer.CurrentTime));
                Scrambler.Scramble();
            };
        }
    }
}
