using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TimetableDesigner.Graphics.Commands
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> action;
        private Predicate<object> predicate;

        public CommandBase(Action<object> action, Predicate<object> predicate){
            this.action = action;
            this.predicate = predicate;
        }

        public bool CanExecute(object parameter)
        {
            return predicate(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }

        public void ExecutionChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public static readonly CommandBase Empty = new CommandBase((o) => { }, (o) => false);

    }
}
