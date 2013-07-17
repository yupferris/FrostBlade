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
        public AlgorithmPractice AlgorithmPractice { get; private set; }

        public AlgorithmPracticeView()
        {
            InitializeComponent();

            DataContextChanged += (sender, e) => AlgorithmPractice = (e.NewValue as ObjectDataProvider).Data as AlgorithmPractice;
        }
    }
}
