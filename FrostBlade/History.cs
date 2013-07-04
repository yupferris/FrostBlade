using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FrostBlade
{
    public class History
    {
        public enum EntryPenaltyState
        {
            None,
            PlusTwo,
            Dnf,
        }

        public class Entry : NotifyPropertyChanged
        {
            readonly History _history;

            readonly double _time;
            public double Time { get { return _time; } }

            EntryPenaltyState _penaltyState;
            public EntryPenaltyState PenaltyState
            {
                get { return _penaltyState; }
                set
                {
                    if (value != _penaltyState)
                    {
                        _penaltyState = value;
                        updateDisplayValue();
                        OnPropertyChanged("PenaltyState");
                    }
                }
            }

            string _displayValue;
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

            public Entry(History history, double time, EntryPenaltyState penaltyState = EntryPenaltyState.None)
            {
                _history = history;
                _time = time;
                _penaltyState = penaltyState;

                updateDisplayValue();
            }

            public void Remove()
            {
                _history.Remove(this);
            }

            void updateDisplayValue()
            {
                string displayValue;
                switch (PenaltyState)
                {
                    case EntryPenaltyState.PlusTwo: displayValue = toFormattedString(Time + 2.0) + "+"; break;
                    case EntryPenaltyState.Dnf: displayValue = toFormattedString(Time) + " (DNF)"; break;
                    default: displayValue = toFormattedString(Time); break;
                }
                DisplayValue = displayValue;
            }

            string toFormattedString(double value)
            {
                return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:F2}", value);
            }
        }

        readonly ObservableCollection<Entry> _entries = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> Entries { get { return _entries; } }

        Command _clearCommand;
        public Command ClearCommand
        {
            get
            {
                if (_clearCommand == null)
                    _clearCommand = new Command(x => Clear());
                return _clearCommand;
            }
        }

        public void Clear()
        {
            Entries.Clear();
        }

        public void Remove(Entry entry)
        {
            Entries.Remove(entry);
        }
    }
}
