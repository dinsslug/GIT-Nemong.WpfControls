﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Nemont.Demo.Services
{
    public class RelayCommand : ICommand
    {
        private readonly Action executeNonParameter;
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        // Constructors
        public RelayCommand(Action execute)
        {
            if (execute == null) {
                throw new ArgumentNullException("Execute delegate action is null.");
            }
            executeNonParameter = execute;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) {
                throw new ArgumentNullException("Execute delegate action is null.");
            }
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (execute != null) {
                execute(parameter);
            }
            else {
                executeNonParameter();
            }
        }
    }
}
