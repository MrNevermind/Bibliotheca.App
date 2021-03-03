using Bibliotheca.App.Base;
using Bibliotheca.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Bibliotheca.App.Models
{
    public class AppBook : Book
    {
        public ICommand EditBookCommand { get; set; }
        public ICommand TakeBookCommand { get; set; }
        public ICommand ReturnBookCommand { get; set; }
        public ICommand DeleteBookCommand { get; set; }
    }
}
