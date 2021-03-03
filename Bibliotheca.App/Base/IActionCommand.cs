using System;
using System.Windows.Input;

namespace Bibliotheca.App.Base
{
    public interface IActionCommand : ICommand
    {
        event Action<IActionCommand, bool> ExecutingChanged;
        void SetCanExecute(bool canExecute);
    }
}
