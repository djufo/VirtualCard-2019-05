using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCard.Lib.Models
{
    public class Account
    {
        public List<Card> Cards { get; private set; }
        public decimal Amount { get; internal set; }

        internal Account(List<Card> cards, decimal amount)
        {
            this.Cards = cards;
            this.Amount = amount;
        }
    }
}
