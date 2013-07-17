using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrostBlade.Algorithms;

namespace FrostBlade
{
    public class AlgorithmPractice : NotifyPropertyChanged
    {
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

        public AlgorithmPractice(AlgorithmType filter)
        {
            Filter = filter;
        }
    }
}
