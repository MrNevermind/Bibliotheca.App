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
    public class BooksViewModel : BaseViewModel
    {
        BooksModel BooksModel { get; set; }
        Dictionary<int, BookStatus> BookStatuses;
        public BooksViewModel()
        {
            NewBookCommand = new ActionCommand((o) => CreateBook());
            CheckStatusCommand = new ActionCommand((o) => CheckStatus());
            BooksModel = new BooksModel();
            LoadBooks();
        }

        public void LoadBooks()
        {
            AppBook[] appBooks = BooksModel.GetBooks().Select(b => new AppBook() {
                Id = b.Id.Value,
                Author = b.Author,
                Title = b.Title,
                Status = b.Status,
                EditBookCommand = new ActionCommand((o) => EditBook(b.Id.Value)),
                TakeBookCommand = new ActionCommand((o) => TakeBook(b.Id.Value)),
                ReturnBookCommand = new ActionCommand((o) => ReturnBook(b.Id.Value)),
                DeleteBookCommand = new ActionCommand((o) => DeleteBook(b.Id.Value))
            }).ToArray();

            BookStatuses = appBooks.ToDictionary(b => b.Id.Value, b => b.Status);

            Books.Clear();

            foreach (AppBook appBook in appBooks)
            {
                Books.Add(appBook);
            }
        }
        public void CheckStatus()
        {
            if(!String.IsNullOrWhiteSpace(BookTitle))
            {
                BookCheckResult = BooksModel.CheckBookStatus(BookTitle);
            }
        }

        public void CreateBook()
        {
            Window window = GetWindow(500, 600);

            BookFormViewModel formViewModel = new BookFormViewModel();
            formViewModel.closeWindow = closeWindow;
            window.Content = formViewModel;

            window.ShowDialog();
            LoadBooks();
        }
        public void EditBook(int id)
        {
            Book book = BooksModel.GetBook(id);

            Window window = GetWindow(500, 600);

            BookFormViewModel formViewModel = new BookFormViewModel();
            formViewModel.Id = book.Id.Value;
            formViewModel.Author = book.Author;
            formViewModel.Title = book.Title;
            if (book.Cover != null)
                formViewModel.ImageSource = BooksModel.GetCoverFromBytes(book.Cover);
            formViewModel.closeWindow = closeWindow;
            window.Content = formViewModel;

            window.ShowDialog();
            LoadBooks();
        }
        public void TakeBook(int id)
        {
            if(BookStatuses[id] == BookStatus.Available)
            {
                Window window = GetWindow(150);
                BookTakeViewModel formViewModel = new BookTakeViewModel();
                formViewModel.closeWindow = closeWindow;
                formViewModel.BookId = id;
                window.Content = formViewModel;
                window.ShowDialog();
                LoadBooks();
            }
        }
        public void ReturnBook(int id)
        {
            if (BookStatuses[id] == BookStatus.Taken)
            {
                BooksModel.ReturnBook(id);
                LoadBooks();
            }
        }
        public void DeleteBook(int id)
        {
            var r = MessageBox.Show("Are you sure you want to delete this book?","Are you sure?", MessageBoxButton.YesNo);
            if(r == MessageBoxResult.Yes)
            {
                BooksModel.DeleteBook(id);
                LoadBooks();
            }
        }

        public ObservableCollection<AppBook> Books { get; set; } = new ObservableCollection<AppBook>();

        public ICommand NewBookCommand { get; set; }

        private string bookTitle;
        public string BookTitle
        {
            get
            {
                return bookTitle;
            }
            set
            {
                bookTitle = value;
                NotifyPropertyChanged();
            }
        }

        private string bookCheckResult;
        public string BookCheckResult
        {
            get
            {
                return bookCheckResult;
            }
            set
            {
                bookCheckResult = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand CheckStatusCommand { get; set; }
    }
}
