using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FrostBlade
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        readonly Action<object> _executeAction;
        readonly Predicate<object> _canExecute;

        public Command(Action<object> executeAction, Predicate<object> canExecute = null)
        {
            if (executeAction == null)
                throw new ArgumentNullException("executeAction");
            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        public void OnCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
