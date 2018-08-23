// using System;
// using System.Security.Cryptography;
// using System.Text;
// using System.Security.Cryptography.X509Certificates;

// namespace enki.common.core.cryptography
// {
//     //http://stackoverflow.com/questions/21685939/how-do-i-set-the-public-and-private-key-on-rsaparameters-to-use-for-rsacryptoser
//     //https://dusted.codes/how-to-use-rsa-in-dotnet-rsacryptoserviceprovider-vs-rsacng-and-good-practise-patterns
    
//     /// <summary>
//     /// Permite criptogradia simétrica com uso de chaves Pública/Privada.
//     /// </summary>
//     public class RsaAsymmetricCryptography : IDisposable
//     {
//         private readonly RSA _rsa;
//         private Encoding encodingToUse = new UTF8Encoding();

//         public RsaAsymmetricCryptography()
//         {
//             _rsa = RSA.Create();

//             var parameters = new RSAParameters();
            
//             parameters.Exponent = StringUtils.GetBytes("PublicKey"); 
//             parameters.P = StringUtils.GetBytes("PublicKey");

//             parameters.Modulus = StringUtils.GetBytes("PrivateKey");
//             parameters.D = StringUtils.GetBytes("PrivateKey");
//             _rsa.ImportParameters(parameters);
//         }

//         public RsaAsymmetricCryptography(RSA rsa)
//         {
//             _rsa = rsa;
//         }

//         public void SetParameters(RSAParameters parameters)
//         {
//             _rsa.ImportParameters(parameters);
//         }

//         public string Encrypt(string message)
//         {
//             byte[] data = StringUtils.GetBytes(message);
//             return Convert.ToBase64String(_rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1));
//         }

//         public string Encrypt(string message, RSAEncryptionPadding padding)
//         {
//             byte[] data = StringUtils.GetBytes(message);
//             return Convert.ToBase64String(_rsa.Encrypt(data, padding));
//         }

//         public string Decrypt(string encodedData)
//         {
//             var data = Convert.FromBase64String(encodedData);
//             return StringUtils.GetString(_rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1));
//         }

//         public string Decrypt(string encodedData, RSAEncryptionPadding padding)
//         {
//             var data = Convert.FromBase64String(encodedData);
//             return StringUtils.GetString(_rsa.Decrypt(data, padding));
//         }

//         public void Dispose()
//         {
//             _rsa.Dispose();
//         }
//     }
// }