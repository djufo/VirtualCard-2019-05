using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using VirtualCard.Lib.Models;
using VirtualCard.Lib.Exceptions;

namespace VirtualCard.Lib.Services
{
    public class AccountService : IAccountService
    {
        private readonly List<Account> _accounts;
        public ReadOnlyCollection<Account> Accounts { get; private set; }

        private readonly IPinService _pinService;

        public AccountService(IPinService pinService)
        {
            this._pinService = pinService;

            this._accounts = new List<Account>();

            this.Accounts = new ReadOnlyCollection<Account>(this._accounts);
        }

        public void AddAccount(Account account)
        {
            Card card = this._accounts.SelectMany(x => x.Cards).Join(account.Cards, card1 => card1.Number, card2 => card2.Number, (cn1, cn2) => cn2).FirstOrDefault();
            if(card != null)
            {
                throw new CardExistsException(card.Number);
            }

            this._accounts.Add(account);
        }

        public Account GetAccount(int pin, string cardNumber)
        {
            string pinHash = this._pinService.GetHash(pin);
            try
            {
                Account result = this.Accounts
                    .SelectMany(account => account.Cards.Select(card => new { account, card }))
                    .Single(x => x.card.Number == cardNumber && x.card.PinHash == pinHash)
                    .account;

                return result;
            }
            catch (InvalidOperationException)
            {
                throw new CardException();
            }
        }
    }
}
