using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FrostBlade
{
    public class Timer : NotifyPropertyChanged
    {
        public double CurrentTime { get { return _stopwatch.Elapsed.TotalSeconds; } }

        string _displayValue = "Ready";
        public string DisplayValue
        {
            get { return _displayValue; }
            private set
            {
                if (value != _displayValue)
                {
                    _displayValue = value;
                    OnPropertyChanged("DisplayValue");
                }
            }
        }

        public event EventHandler Stopped;

        readonly Stopwatch _stopwatch = new Stopwatch();
        readonly System.Timers.Timer _timer = new System.Timers.Timer(10.0);

        public Timer()
        {
            _timer.Elapsed += (sender, e) => updateDisplayValue();
        }

        public void StartStop()
        {
            if (!_stopwatch.IsRunning)
            {
                start();
            }
            else
            {
                stop();
            }
        }

        void start()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
            _timer.Start();
        }

        void stop()
        {
            _stopwatch.Stop();
            _timer.Stop();
            updateDisplayValue();
            onStopped();
        }

        void onStopped()
        {
            if (Stopped != null)
                Stopped(this, EventArgs.Empty);
        }

        void updateDisplayValue()
        {
            DisplayValue = CurrentTime.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
