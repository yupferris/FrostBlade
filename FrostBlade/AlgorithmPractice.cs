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

        AlgorithmType _filter;
        public AlgorithmType Filter
        {
            get { return _filter; }
            set
            {
                if (value != _filter)
                {
                    _filter = value;
                    OnPropertyChanged("Filter");
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

        readonly Timer _timer = new Timer();
        State _state = State.Idle;
        int _countdown;
        readonly System.Timers.Timer _countdownTimer = new System.Timers.Timer(1000.0);

        public AlgorithmPractice(AlgorithmType filter)
        {
            Filter = filter;

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
                    var algs = Database.Algorithms.Where(x => x.Type == Filter).ToArray();
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
