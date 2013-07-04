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

namespace FrostBlade.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public event EventHandler SpacePressed;

        public MainWindow()
        {
            InitializeComponent();

            var database = new Algorithms.Database("Database");

            _standardSolving.MainWindow = this;
            _algorithmIndex.Database = database;
        }

        void previewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && SpacePressed != null)
            {
                SpacePressed(this, EventArgs.Empty);
                e.Handled = true;
            }
        }
    }
}
