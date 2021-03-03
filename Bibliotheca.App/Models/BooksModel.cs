using Bibliotheca.App.Base;
using Bibliotheca.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheca.App.Models
{
    public class BooksModel : BaseModel
    {
        public BooksModel() : base()
        {
        }

        public Book[] GetBooks()
        {
            return BibliothecaClient.ExecuteGet<Book[]>("books").GetAwaiter().GetResult();
        }
        public Book GetBook(int id)
        {
            return BibliothecaClient.ExecuteGet<Book>($"books/{id}").GetAwaiter().GetResult();
        }
        public string CheckBookStatus(string title)
        {
            var status = BibliothecaClient.ExecuteGet<BookStatus?>($"books/{title}/check").GetAwaiter().GetResult();
            if (status.HasValue)
            {
                if (status.Value == BookStatus.Available)
                    return "Book is available for the taking";
                else
                    return "Book is already taken";
            }
            else
            {
                return "Book not found";
            }
        }
        public void CreateBook(Book book)
        {
            BibliothecaClient.ExecutePost("books", book);
        }
        public void UpdateBook(int id, Book book)
        {
            BibliothecaClient.ExecutePut($"books/{id}", book);
        }
        public void DeleteBook(int id)
        {
            BibliothecaClient.ExecuteDelete($"books/{id}");
        }
        public void ReturnBook(int id)
        {
            BibliothecaClient.ExecutePost($"books/{id}/return", null);
        }
        public byte[] GetCoverBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
        public string GetCoverFromBytes(byte[] bytes)
        {
            string fileName = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".jpeg");
            File.WriteAllBytes(fileName, bytes);
            return fileName;
        }
    }
}
