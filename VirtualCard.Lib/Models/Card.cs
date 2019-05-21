using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCard.Lib.Models
{
    public class Card
    {
        public string Number { get; private set; }
        public string PinHash { get; private set; }

        internal Card(string number, string pinHash)
        {
            this.Number = number;
            this.PinHash = pinHash;
        }
    }
}
