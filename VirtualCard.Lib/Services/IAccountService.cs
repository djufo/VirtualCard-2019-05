using System.Collections.ObjectModel;
using VirtualCard.Lib.Models;

namespace VirtualCard.Lib.Services
{
    public interface IAccountService
    {
        ReadOnlyCollection<Account> Accounts { get; }

        void AddAccount(Account account);
        Account GetAccount(int pin, string cardNumber);
    }
}