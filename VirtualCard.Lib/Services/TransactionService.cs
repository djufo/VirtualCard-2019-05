using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VirtualCard.Lib.Models;
using VirtualCard.Lib.Exceptions;

namespace VirtualCard.Lib.Services
{
    public class TransactionService : ITransactionService
    {
        private static readonly object _lock = new object();
        private readonly IAccountService _accountService;
        public TransactionService(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        public Response Submit(Request request)
        {
            Account account = this._accountService.GetAccount(request.Pin, request.CardNumber);
            decimal computed = account.Amount + request.Amount;

            lock (_lock)
            {
                if (request.Amount < 0 && computed < 0)
                {
                    throw new InsufficientBalance();
                }
                account.Amount = computed;

                return new Response { Balance = account.Amount, Success = true };
            }

        }
    }
}
