using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FrostBlade.Algorithms;

namespace FrostBlade.UI
{
    /// <summary>
    /// Interaction logic for AlgorithmPractice.xaml
    /// </summary>
    public partial class AlgorithmPractice : UserControl
    {
        Database _database;
        public Database Database
        {
            get { return _database; }
            set
            {
                if (value != _database)
                {
                    if (_database != null)
                        _database.Algorithms.CollectionChanged -= databaseAlgorithmsCollectionChanged;
                    _database = value;
                    if (_database != null)
                    {
                        _database.Algorithms.CollectionChanged += databaseAlgorithmsCollectionChanged;
                        reflectDatabaseAlgorithms();
                    }
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
                    // TODO
                }
            }
        }

        public AlgorithmPractice()
        {
            InitializeComponent();
        }

        void databaseAlgorithmsCollectionChanged(object sender, EventArgs e)
        {
            reflectDatabaseAlgorithms();
        }

        void reflectDatabaseAlgorithms()
        {
            // TODO
        }
    }
}
