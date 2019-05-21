using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace VirtualCard.Lib.Services
{
    public class PinService : IPinService
    {
        const string SALT = "ABC1234";
        readonly static Encoding Encoding = System.Text.ASCIIEncoding.ASCII;

        public string GetHash(int pin)
        {
            byte[] buffer = Encoding.GetBytes(pin + SALT);

            MD5 md5 = MD5.Create();
            buffer = md5.ComputeHash(buffer);

            string result = System.Convert.ToBase64String(buffer);
            return result;
        }
    }
}
