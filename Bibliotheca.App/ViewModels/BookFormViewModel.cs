using Bibliotheca.App.Base;
using Bibliotheca.App.Models;
using Bibliotheca.Library;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bibliotheca.App.ViewModels
{
    public class BookFormViewModel : BaseViewModel
    {
        BooksModel BooksModel { get; set; }
        public BookFormViewModel()
        {
            BooksModel = new BooksModel();
            SaveCommand = new ActionCommand((o) => Save());
            CancelCommand = new ActionCommand((o) => Cancel());
            BrowseImageCommand = new ActionCommand((o) => BrowseImage());
        }

        public void BrowseImage()
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "*.jpeg|*.jpg";
            var r = dialog.ShowDialog();
            if(r.HasValue && r.Value)
                ImagePath = dialog.FileNames.First();
        }
        public void SetImage(string path)
        {

        }
        public void Save()
        {
            Book book = new Book()
            {
                Author = Author,
                Title = Title,
                Status = BookStatus.Available
            };

            if (!String.IsNullOrWhiteSpace(ImagePath))
                book.Cover = BooksModel.GetCoverBytes(ImagePath);

            if (Id.HasValue)
            {
                BooksModel.UpdateBook(Id.Value, book);
            }
            else
            {
                BooksModel.CreateBook(book);
            }
            closeWindow();
        }
        public void Cancel()
        {
            closeWindow();
        }

        public int? Id { get; set; }

        private string author;
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                NotifyPropertyChanged();
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get
            {
                return imagePath;
            }
            set
            {
                imagePath = value;
                ImageSource = value;
                NotifyPropertyChanged();
            }
        }

        private string imageSource;
        public string ImageSource
        {
            get
            {
                return imageSource;
            }
            set
            {
                imageSource = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand BrowseImageCommand { get; set; }
    }
}
