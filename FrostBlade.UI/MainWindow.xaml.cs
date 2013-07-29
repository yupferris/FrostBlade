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
using FrostBlade;
using FrostBlade.Algorithms;

namespace FrostBlade.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var database = new Database("Database");
            UserData userData;
            try
            {
                userData = UserData.Load("userdata.xml");
            }
            catch (Exception)
            {
                userData = new UserData();
            }

            _standardSolvingView.MainWindow = this;

            _ollPracticeView.MainWindow = this;
            _ollPracticeView.AlgorithmPractice.Database = database;

            _pllPracticeView.MainWindow = this;
            _pllPracticeView.AlgorithmPractice.Database = database;

            _algorithmIndex.Database = database;
        }
    }
}
