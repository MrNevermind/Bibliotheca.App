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
    public class AccountTakenBooksViewModel : BaseViewModel
    {
        AccountsModel AccountsModel { get; set; }
        BooksModel BooksModel { get; set; }
        int AccountId { get; set; }
        public AccountTakenBooksViewModel(int accountId)
        {
            AccountId = accountId;
            AccountsModel = new AccountsModel();
            BooksModel = new BooksModel();
            CloseCommand = new ActionCommand((o) => Close());
            LoadBooks();
        }

        public void LoadBooks()
        {
            AppBook[] appBooks = AccountsModel.GetTakenBooks(AccountId).Select(b => new AppBook()
            {
                Id = b.Id.Value,
                Author = b.Author,
                Title = b.Title,
                Status = b.Status,
                ReturnBookCommand = new ActionCommand((o) => ReturnBook(b.Id.Value))
            }).ToArray();

            Books.Clear();

            foreach(AppBook book in appBooks)
            {
                Books.Add(book);
            }
        }
        public void ReturnBook(int id)
        {
            BooksModel.ReturnBook(id);
            LoadBooks();
        }

        public void Close()
        {
            closeWindow();
        }

        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        public ICommand CloseCommand { get; set; }
    }
}
