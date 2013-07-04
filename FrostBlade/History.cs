using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FrostBlade
{
    public class History : NotifyPropertyChanged
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
            public double TimeIncludingPenalty { get { return _time + (PenaltyState == EntryPenaltyState.PlusTwo ? 2.0 : 0.0); } }

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
                    case EntryPenaltyState.PlusTwo: displayValue = toFormattedString(TimeIncludingPenalty) + "+"; break;
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

        string _stats;
        public string Stats
        {
            get { return _stats; }
            set
            {
                if (value != _stats)
                {
                    _stats = value;
                    OnPropertyChanged("Stats");
                }
            }
        }

        public History()
        {
            Entries.CollectionChanged += (sender, e) =>
                {
                    if (e.NewItems != null)
                    {
                        foreach (Entry entry in e.NewItems)
                        {
                            entry.PropertyChanged += (sender2, e2) =>
                                {
                                    if (e2.PropertyName == "PenaltyState")
                                        updateStats();
                                };
                        }
                    }
                    updateStats();
                };
            updateStats();
        }

        public void Clear()
        {
            Entries.Clear();
        }

        public void Remove(Entry entry)
        {
            Entries.Remove(entry);
        }

        void updateStats()
        {
            var sb = new StringBuilder();

            if (Entries.Count == 0)
            {
                sb.Append("N/A");
            }
            else
            {
                int count = 0;
                double? bestTime = null;
                double? worstTime = null;
                foreach (var entry in Entries.Where(e => e.PenaltyState != EntryPenaltyState.Dnf))
                {
                    count++;
                    if (!bestTime.HasValue || entry.TimeIncludingPenalty < bestTime)
                        bestTime = entry.TimeIncludingPenalty;
                    if (!worstTime.HasValue || entry.TimeIncludingPenalty > worstTime)
                        worstTime = entry.TimeIncludingPenalty;
                }
                sb.AppendLine("Solves: " + count + "/" + Entries.Count);

                if (bestTime.HasValue && worstTime.HasValue)
                {
                    sb.AppendLine("Best time: " + toFormattedString(bestTime.Value));
                    sb.AppendLine("Worst time: " + toFormattedString(worstTime.Value));
                }

                count = 0;
                double total = 0.0;
                double? avg5 = null;
                double? avg12 = null;
                foreach (var entry in Entries.Reverse().Where(e => e.PenaltyState != EntryPenaltyState.Dnf))
                {
                    count++;
                    total += entry.TimeIncludingPenalty;

                    switch (count)
                    {
                        case 5: avg5 = total / 5.0; break;
                        case 12: avg12 = total / 12.0; break;
                    }
                }
                if (count > 0)
                {
                    if (avg5.HasValue)
                    {
                        sb.AppendLine();
                        sb.AppendLine("Current avg5: " + toFormattedString(avg5.Value));
                    }

                    if (avg12.HasValue)
                    {
                        sb.AppendLine();
                        sb.AppendLine("Current avg12: " + toFormattedString(avg12.Value));
                    }

                    double avg = total / (double)count;
                    sb.AppendLine();
                    sb.AppendLine("Session avg: " + toFormattedString(avg));
                }
            }

            Stats = sb.ToString();
        }

        string toFormattedString(double value)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:F2}", value);
        }
    }
}
