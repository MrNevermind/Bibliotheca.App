using Bibliotheca.App.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bibliotheca.App.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            ShowBooksCommand = new ActionCommand((o) => ShowBooks());
            ShowAccountsCommand = new ActionCommand((o) => ShowAccounts());
        }

        public void ShowBooks()
        {
            MainContent = new BooksViewModel();
        }

        public void ShowAccounts()
        {
            MainContent = new AccountsViewModel();
        }

        private object mainContent;
        public object MainContent
        {
            get
            {
                return mainContent;
            }
            set
            {
                mainContent = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand ShowBooksCommand { get; set; }
        public ICommand ShowAccountsCommand { get; set; }
    }
}
