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
    /// Interaction logic for AlgorithmIndex.xaml
    /// </summary>
    public partial class AlgorithmIndex : UserControl
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
                        databaseAlgorithmsCollectionChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public AlgorithmIndex()
        {
            InitializeComponent();
        }

        void databaseAlgorithmsCollectionChanged(object sender, EventArgs e)
        {
            _ollChildrenContainer.Children.Clear();
            foreach (var algorithm in Database.Algorithms.Where(p => p.Type == AlgorithmType.Oll))
                _ollChildrenContainer.Children.Add(new Views.AlgorithmView(algorithm));
            _pllChildrenContainer.Children.Clear();
            foreach (var algorithm in Database.Algorithms.Where(p => p.Type == AlgorithmType.Pll))
                _pllChildrenContainer.Children.Add(new Views.AlgorithmView(algorithm));
        }
    }
}
