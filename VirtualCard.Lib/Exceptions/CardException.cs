using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCard.Lib.Exceptions
{
    public class CardException: Exception
    {
        public CardException(): base("Invalid card or pin") { }
    }
}
