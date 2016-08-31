using System;
using System.IO;
using System.Security.Cryptography;

namespace enki.common.cryptography
{
    public class AesSymmetricCryptography
    {
        private string _key, _IV;
        private readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

        public AesSymmetricCryptography(string password = "NoPassWd@01")
        {
            using (var pdb = new Rfc2898DeriveBytes(password, SALT))
            {
                _key = Convert.ToBase64String((pdb.GetBytes(32)));
                _IV = Convert.ToBase64String((pdb.GetBytes(16)));
            }
        }

        public AesSymmetricCryptography(string key, string IV)
        {
            _key = key;
            _IV = IV;
        }

        public string Encrypt(string message)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(_key);
                aesAlg.IV = Convert.FromBase64String(_IV);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(message);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public string GetKey()
        {
            return _key;
        }

        public string GetIV()
        {
            return _IV;
        }

        public string Decrypt(string base64EncodedData)
        {
            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(base64EncodedData)))
            {
                // Check arguments.
                if (base64EncodedData == null || base64EncodedData.Length <= 0)
                    throw new ArgumentNullException("cipherText");
                if (_key == null || _key.Length <= 0)
                    throw new ArgumentNullException("Key");
                if (_IV == null || _IV.Length <= 0)
                    throw new ArgumentNullException("IV");

                // Declare the string used to hold
                // the decrypted text.
                string plaintext = null;

                // Create an Aes object
                // with the specified key and IV.
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Convert.FromBase64String(_key);
                    aesAlg.IV = Convert.FromBase64String(_IV);

                    // Create a decrytor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

                return plaintext;
            }
        }
    }
}
