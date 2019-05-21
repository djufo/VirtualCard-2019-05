using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCard.Lib.Exceptions
{
    public class CardExistsException: Exception
    {
        public CardExistsException(string cardNumber): base(string.Format("Card: {0} already exists", cardNumber)) { }
    }
}
