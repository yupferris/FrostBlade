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
    /// Interaction logic for StandardSolving.xaml
    /// </summary>
    public partial class StandardSolving : UserControl
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
                        _mainWindow.SpacePressed -= mainWindowSpacePressed;
                    _mainWindow = value;
                    if (_mainWindow != null)
                        _mainWindow.SpacePressed += mainWindowSpacePressed;
                }
            }
        }

        readonly Timer _timer;
        readonly History _history;

        public StandardSolving()
        {
            InitializeComponent();

            var scrambler = (Scrambler)getResource("_scrambler");
            _timer = (Timer)getResource("_timer");
            _history = (History)getResource("_history");

            _timer.Stopped += (sender, e) =>
                {
                    _history.Entries.Add(new History.Entry(_history, _timer.CurrentTime));
                    scrambler.Scramble();
                };
        }

        object getResource(string name)
        {
            return ((ObjectDataProvider)Resources[name]).Data;
        }

        void mainWindowSpacePressed(object sender, EventArgs e)
        {
            _timer.StartStop();
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
            if (_history.Entries.Count > 0)
                _history.Entries.Last().PenaltyState = penaltyState;
        }
    }
}
