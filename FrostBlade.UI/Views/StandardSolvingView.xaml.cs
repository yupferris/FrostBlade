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
    /// Interaction logic for StandardSolvingView.xaml
    /// </summary>
    public partial class StandardSolvingView : UserControl
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

        public StandardSolving StandardSolving { get; private set; }

        public StandardSolvingView()
        {
            InitializeComponent();

            DataContextChanged += (sender, e) => StandardSolving = (e.NewValue as ObjectDataProvider).Data as StandardSolving;
        }

        void mainWindowPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (StandardSolving != null && IsVisible && e.Key == Key.Space)
            {
                StandardSolving.Timer.StartStop();
                e.Handled = true;
            }
        }

        void dnfButtonClick(object sender, RoutedEventArgs e)
        {
            updateLastHistoryEntryPenaltyState(History.EntryPenaltyState.Dnf);
        }

        void plusTwoButtonClick(object sender, RoutedEventArgs e)
        {
            updateLastHistoryEntryPenaltyState(History.EntryPenaltyState.PlusTwo);
        }

        void noPenaltyButtonClick(object sender, RoutedEventArgs e)
        {
            updateLastHistoryEntryPenaltyState(History.EntryPenaltyState.None);
        }

        void updateLastHistoryEntryPenaltyState(History.EntryPenaltyState penaltyState)
        {
            if (StandardSolving == null)
                return;

            if (StandardSolving.History.Entries.Count > 0)
                StandardSolving.History.Entries.Last().PenaltyState = penaltyState;
        }
    }
}
