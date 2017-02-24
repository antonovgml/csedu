using System;
using System.Security.Cryptography;
using System.Text;
using Util;

namespace Encryption
{
    /*
     Develop a console application to encrypt and decrypt data using private and public keys.
     */

    public class OneTimeCrypto
    {
        private bool expired = false;

        public string publicKey { get; private set; }
        public string privateKey { get; private set; }
        private RSACryptoServiceProvider rsa;

        private OneTimeCrypto() {
            rsa = new RSACryptoServiceProvider();
            this.publicKey = rsa.ToXmlString(false);
            this.privateKey = rsa.ToXmlString(true);
        }

        public static OneTimeCrypto getInstance()
        {
            return new OneTimeCrypto();
        }

        public string Encrypt(string sourceText) {            
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(sourceText);             
            this.rsa.FromXmlString(this.publicKey);
            byte[] encrypted = rsa.Encrypt(dataToEncrypt, false);
            
            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string encryptedText)
        {
            if (this.expired)
            {
                throw new Exception("This OneTimeCrypto instance has expired. Please create a new instance for every message");
            }

            byte[] dataToDecrypt = Convert.FromBase64String(encryptedText);             
            this.rsa.FromXmlString(this.privateKey);
            string decrypted = Encoding.UTF8.GetString(rsa.Decrypt(dataToDecrypt, false));

            this.expired = true;
            return decrypted;
        }

    }

}