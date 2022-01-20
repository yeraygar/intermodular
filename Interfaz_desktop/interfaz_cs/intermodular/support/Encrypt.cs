using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace intermodular
{
    public static class Encrypt
    {
        /// <summary>
        /// Static Method. Recibe un string y lo devuelve cifrado
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Input cifrado</returns>
        public static string GetSHA256(string input)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding enconding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(enconding.GetBytes(input));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
