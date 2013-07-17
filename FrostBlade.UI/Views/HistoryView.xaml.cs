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
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FrostBlade.UI.Views
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        History _history;
        public History History
        {
            get { return _history; }
            set
            {
                if (value != _history)
                {
                    if (_history != null)
                        _history.Entries.CollectionChanged -= historyEntriesCollectionChanged;
                    _history = value;
                    if (_history != null)
                        _history.Entries.CollectionChanged += historyEntriesCollectionChanged;
                }
            }
        }

        public HistoryView()
        {
            InitializeComponent();

            DataContextChanged += (sender, e) => History = DataContext as History;
        }

        void historyEntriesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (History == null)
                return;

            var entries = History.Entries;
            var children = _childrenContainer.Children;
            if (children.Count > entries.Count)
                children.RemoveRange(entries.Count, children.Count - entries.Count);
            for (int i = 0; i < entries.Count; i++)
            {
                var entry = entries[i];
                if (i < children.Count)
                {
                    var existingView = children[i] as HistoryEntryView;
                    if (existingView != null && entry == existingView.Entry)
                    {
                        continue;
                    }
                    else
                    {
                        children.RemoveAt(i);
                    }
                }
                children.Insert(i, new HistoryEntryView(entry));
            }
        }
    }
}
