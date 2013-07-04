using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FrostBlade.UI
{
    static class App
    {
        [STAThread]
        public static int Main(string[] args)
        {
            try
            {
                return new Application().Run(new MainWindow());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Fatal error");
                return 1;
            }
        }
    }
}
