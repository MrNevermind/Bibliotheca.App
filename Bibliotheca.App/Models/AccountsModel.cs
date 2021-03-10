using Bibliotheca.App.Base;
using Bibliotheca.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliotheca.App.Models
{
    public class AccountsModel : BaseModel
    {
        public Account[] GetAccounts()
        {
            return BibliothecaClient.ExecuteGet<Account[]>("accounts");
        }
        public Account GetAccount(int id)
        {
            return BibliothecaClient.ExecuteGet<Account>($"accounts/{id}");
        }
        public Book[] GetTakenBooks(int id)
        {
            return BibliothecaClient.ExecuteGet<Book[]>($"accounts/{id}/books");
        }
        public void TakeBook(int bookId, int accountId)
        {
            BibliothecaClient.ExecutePost($"accounts/{accountId}/take/{bookId}", null);
        }
        public void CreateAccount(Account account)
        {
            BibliothecaClient.ExecutePost("accounts", account);
        }
        public void UpdateAccount(int id, Account account)
        {
            BibliothecaClient.ExecutePut($"accounts/{id}", account);
        }
        public void DeleteAccount(int id)
        {
            BibliothecaClient.ExecuteDelete($"accounts/{id}");
        }
    }
}
