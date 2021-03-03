using Bibliotheca.App.Base;
using Bibliotheca.App.Models;
using Bibliotheca.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bibliotheca.App.ViewModels
{
    public class BookTakeViewModel : BaseViewModel
    {
        AccountsModel AccountsModel { get; set; }
        public BookTakeViewModel()
        {
            AccountsModel = new AccountsModel();
            TakeCommand = new ActionCommand((o) => Take());
            CancelCommand = new ActionCommand((o) => Cancel());
            LoadAccounts();
        }

        public void LoadAccounts()
        {
            Accounts.Clear();
            foreach (var account in AccountsModel.GetAccounts())
            {
                Accounts.Add(account);
            }
        }

        public void Take()
        {
            AccountsModel.TakeBook(BookId, SelectedAccount.Id.Value);
            closeWindow();
        }

        public void Cancel()
        {
            closeWindow();
        }

        public int BookId { get; set; }

        private Account selectedAccount;
        public Account SelectedAccount
        {
            get
            {
                return selectedAccount;
            }
            set
            {
                selectedAccount = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();

        public ICommand TakeCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}
