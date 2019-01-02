using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TravelManager.VIewModal
{
    
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Func<object,bool> _canExecuteasync;

        private readonly Func<object,Task> _executeasync;
        private readonly Action<object> _execute;


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }   

        public DelegateCommand(Func<object,Task> execute)
            : this(execute, null)
        {
        }
        public DelegateCommand(Action<object> execute)
          : this(execute, null)
        {
        }

        public DelegateCommand(Func<object,Task> execute, Func<object,bool> canExecute )
        {
           
                _executeasync = execute;
                _canExecuteasync = canExecute;
            
           
        }
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
           
                _execute = execute;
                _canExecute = canExecute;
           
        }


        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (_executeasync == null)
                _execute(parameter);
            else
                _executeasync(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}