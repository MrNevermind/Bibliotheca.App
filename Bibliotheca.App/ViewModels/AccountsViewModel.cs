using Bibliotheca.App.Base;
using Bibliotheca.App.Models;
using Bibliotheca.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Bibliotheca.App.ViewModels
{
    public class AccountsViewModel : BaseViewModel
    {
        AccountsModel AccountsModel { get; set; }
        public AccountsViewModel()
        {
            AccountsModel = new AccountsModel();
            NewAccountCommand = new ActionCommand((o) => NewAccount());
            LoadAccounts();
        }

        public void LoadAccounts()
        {
            AppAccount[] appAccounts = AccountsModel.GetAccounts().Select(b => new AppAccount()
            {
                Id = b.Id.Value,
                FirstName = b.FirstName,
                LastName = b.LastName,
                PhoneNumber = b.PhoneNumber,
                EditAccountCommand = new ActionCommand((o) => EditAccount(b.Id.Value)),
                ShowTakenBooksCommand = new ActionCommand((o) => ShowTakenBooks(b.Id.Value)),
                DeleteAccountCommand = new ActionCommand((o) => DeleteAccount(b.Id.Value))
            }).ToArray();

            Accounts.Clear();

            foreach (AppAccount appAccount in appAccounts)
            {
                Accounts.Add(appAccount);
            }
        }

        public void NewAccount()
        {
            Window window = GetWindow(500, 600);

            AccountFormViewModel formViewModel = new AccountFormViewModel();
            formViewModel.closeWindow = closeWindow;
            window.Content = formViewModel;

            window.ShowDialog();
            LoadAccounts();
        }

        public void EditAccount(int id)
        {
            Account account = AccountsModel.GetAccount(id);

            Window window = GetWindow(500, 600);

            AccountFormViewModel formViewModel = new AccountFormViewModel();
            formViewModel.Id = account.Id.Value;
            formViewModel.FirstName = account.FirstName;
            formViewModel.LastName = account.LastName;
            formViewModel.PhoneNumber = account.PhoneNumber;
            formViewModel.closeWindow = closeWindow;
            window.Content = formViewModel;

            window.ShowDialog();
            LoadAccounts();
        }
        public void ShowTakenBooks(int id)
        {
            Window window = GetWindow(500, 600);

            AccountTakenBooksViewModel formViewModel = new AccountTakenBooksViewModel(id);
            formViewModel.closeWindow = closeWindow;
            window.Content = formViewModel;

            window.ShowDialog();
        }
        public void DeleteAccount(int id)
        {
            var r = MessageBox.Show("Are you sure you want to delete this account?", "Are you sure?", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                AccountsModel.DeleteAccount(id);
                LoadAccounts();
            }
        }

        public ObservableCollection<AppAccount> Accounts { get; set; } = new ObservableCollection<AppAccount>();

        public ICommand NewAccountCommand { get; set; }
    }
}
