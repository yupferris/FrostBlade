﻿using System;
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
    /// Interaction logic for HistoryEntryView.xaml
    /// </summary>
    public partial class HistoryEntryView : UserControl
    {
        readonly History.Entry _entry;
        public History.Entry Entry { get { return _entry; } }

        public HistoryEntryView(History.Entry entry)
        {
            InitializeComponent();

            DataContext = _entry = entry;
        }

        void mouseDown(object sender, MouseButtonEventArgs e)
        {
            Entry.Remove();
        }
    }
}
