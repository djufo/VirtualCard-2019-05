using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCard.Lib.Exceptions
{
    public class InsufficientBalance: Exception
    {
        public InsufficientBalance() : base("Insufficient balance") { }
    }
}
