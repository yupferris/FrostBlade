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

namespace FrostBlade.UI.Views
{
    /// <summary>
    /// Interaction logic for AlgorithmPracticeView.xaml
    /// </summary>
    public partial class AlgorithmPracticeView : UserControl
    {
        MainWindow _mainWindow;
        public MainWindow MainWindow
        {
            get { return _mainWindow; }
            set
            {
                if (value != _mainWindow)
                {
                    if (_mainWindow != null)
                        _mainWindow.PreviewKeyDown -= mainWindowPreviewKeyDown;
                    _mainWindow = value;
                    if (_mainWindow != null)
                        _mainWindow.PreviewKeyDown += mainWindowPreviewKeyDown;
                }
            }
        }

        public AlgorithmPractice AlgorithmPractice { get; private set; }

        public AlgorithmPracticeView()
        {
            InitializeComponent();

            DataContextChanged += (sender, e) => AlgorithmPractice = (e.NewValue as ObjectDataProvider).Data as AlgorithmPractice;
        }

        void mainWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (AlgorithmPractice != null && IsVisible && e.Key == Key.Space)
            {
                AlgorithmPractice.StartStop();
                e.Handled = true;
            }
        }
    }
}
