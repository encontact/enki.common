// using Xunit;

// namespace enki.common.cryptography.test
// {
//     public class AsymmetricCryptographyTests
//     {
//         [Fact]
//         public void RsaAsymmetricCryptographyTest()
//         {
//             var message = "TESTE !@#$#$%$¨%&¨*&**&(257386";
//             var crypto = new RsaAsymmetricCryptography();
//             var cryptedData = crypto.Encrypt(message);
//             // var publicKey = crypto.GetPublicKey();
//             // var privateKey = crypto.GetPrivateKey();
//             //var crypto2 = new RsaAsymmetricCryptography(privateKey, publicKey);
//             var crypto2 = new RsaAsymmetricCryptography();
            
//             // Valida descriptografia a partir do primeiro objeto.
//             Assert.Equal(message, crypto.Decrypt(cryptedData));
//             // Valida descriptografia a partir de outra classe criada com as mesmas chaves
//             Assert.Equal(message, crypto2.Decrypt(cryptedData));
//             // // Valida que duas chaves com mesmo texto são criadas diferente
//             // Assert.NotEqual(cryptedData, crypto2.Encrypt(message));


//             // var messageLong = "{\"ClientId\":1,\"DueDate\":\"2020 - 10 - 10T00: 00:00\",\"EmailLicenses\":1,\"PhoneLicenses\":0,\"ChatLicenses\":12,\"HasVirtualAssistant\":true}";
//             // try
//             // {
//             //     var cryptedLongData = crypto.Encrypt(messageLong);
//             //     Assert.Fail("Não poderia decriptogradar uma mensagem tão grande.");
//             // }
//             // catch
//             // {
//             //     Assert.IsTrue(true);
//             // }
//         }
//     }
// }