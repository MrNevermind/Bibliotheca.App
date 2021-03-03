using Bibliotheca.App.Base;
using Bibliotheca.App.Models;
using Bibliotheca.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bibliotheca.App.ViewModels
{
    public class AccountFormViewModel : BaseViewModel
    {
        AccountsModel AccountsModel { get; set; }
        public AccountFormViewModel()
        {
            AccountsModel = new AccountsModel();
            SaveCommand = new ActionCommand((o) => Save());
            CancelCommand = new ActionCommand((o) => Cancel());
        }


        public void Save()
        {
            Account account = new Account()
            {
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber
            };

            if (Id.HasValue)
            {
                AccountsModel.UpdateAccount(Id.Value, account);
            }
            else
            {
                AccountsModel.CreateAccount(account);
            }
            closeWindow();
        }

        public int? Id { get; set; }

        private string firstName;
        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                firstName = value;
                NotifyPropertyChanged();
            }
        }
        private string lastName;
        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                lastName = value;
                NotifyPropertyChanged();
            }
        }
        private string phoneNumber;
        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
                NotifyPropertyChanged();
            }
        }

        public void Cancel()
        {
            closeWindow();
        }


        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
    }
}
