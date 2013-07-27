using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrostBlade.Algorithms;
using System.Windows.Media.Imaging;

namespace FrostBlade
{
    public class AlgorithmPractice : NotifyPropertyChanged
    {
        enum State
        {
            Idle,
            Countdown,
            Running
        }

        class AlgorithmStats
        {
            public readonly List<double> LastSolves = new List<double>();

            public void Add(double time)
            {
                LastSolves.Insert(0, time);
                if (LastSolves.Count > 5)
                    LastSolves.Remove(LastSolves.Last());
            }
        }

        Database _database;
        public Database Database
        {
            get { return _database; }
            set
            {
                if (value != _database)
                {
                    _database = value;
                    OnPropertyChanged("Database");
                }
            }
        }

        BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }
            set
            {
                if (value != _image)
                {
                    _image = value;
                    OnPropertyChanged("Image");
                }
            }
        }

        bool _showImage;
        public bool ShowImage
        {
            get { return _showImage; }
            set
            {
                if (value != _showImage)
                {
                    _showImage = value;
                    OnPropertyChanged("ShowImage");
                }
            }
        }

        string _displayValue = "Ready";
        public string DisplayValue
        {
            get { return _displayValue; }
            set
            {
                if (value != _displayValue)
                {
                    _displayValue = value;
                    OnPropertyChanged("DisplayValue");
                }
            }
        }

        readonly AlgorithmType _filter;
        readonly Timer _timer = new Timer();
        State _state = State.Idle;
        int _countdown;
        readonly System.Timers.Timer _countdownTimer = new System.Timers.Timer(1000.0);

        readonly Dictionary<Algorithm, AlgorithmStats> _algorithmStats = new Dictionary<Algorithm, AlgorithmStats>();

        public AlgorithmPractice(AlgorithmType filter)
        {
            _filter = filter;

            _timer.PropertyChanged += (sender, e) =>
                {
                    if (_state == State.Running && e.PropertyName == "DisplayValue")
                        DisplayValue = _timer.DisplayValue;
                };

            _countdownTimer.Elapsed += (sender, e) =>
                {
                    if (_state != State.Countdown)
                        return;

                    _countdown--;
                    if (_countdown < 1)
                    {
                        _countdownTimer.Stop();
                        _state = State.Running;
                        _timer.StartStop();
                    }
                    else
                    {
                        if (_countdown == 1)
                            ShowImage = true;
                        DisplayValue = _countdown.ToString();
                    }
                };
        }

        public void StartStop()
        {
            switch (_state)
            {
                case State.Idle:
                    var algs = Database.Algorithms.Where(x => x.Type == _filter).ToArray();
                    var random = new Random();
                    Image = algs[random.Next(algs.Length)].Image;
                    ShowImage = false;
                    _countdown = 3;
                    DisplayValue = _countdown.ToString();
                    _state = State.Countdown;
                    _countdownTimer.Start();
                    break;

                case State.Countdown:
                    _countdownTimer.Stop();
                    _state = State.Idle;
                    DisplayValue = "Ready";
                    break;

                case State.Running:
                    _timer.StartStop();
                    _state = State.Idle;
                    break;
            }
        }
    }
}
