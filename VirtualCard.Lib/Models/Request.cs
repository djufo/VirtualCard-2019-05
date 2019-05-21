using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualCard.Lib.Models
{
    public class Request
    {
        public string CardNumber { get; set; }
        public int Pin { get; set; }
        public decimal Amount { get; set; }
    }
}
