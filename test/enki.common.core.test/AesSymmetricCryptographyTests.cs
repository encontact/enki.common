using Xunit;

namespace enki.common.core.cryptography.test
{
    public class AesSymmetricCryptographyTests
    {
        [Fact]
        public void AesSymmetricCryptographyTest()
        {
           // Valida criptografia
            var message = "TESTE !@#$#$%$¨%&¨*&**&(257386";
            var crypto = new AesSymmetricCryptography();
            var cryptedData = crypto.Encrypt(message);
            Assert.Equal("p8BMWuthjnD61Hy0q+iP+VyafgWmV7TOgVPKjdO+sMbGzP5B/rfNv3i1+AXC1RCa", cryptedData);

            // Valida descriptografia
            var key = crypto.GetKey();
            var iv = crypto.GetIV();
            var crypto2 = new AesSymmetricCryptography(key, iv);
            Assert.Equal(message, crypto.Decrypt(cryptedData));
            // Valida descriptografia a partir de outra classe criada com as mesmas chaves
            Assert.Equal(message, crypto2.Decrypt(cryptedData));
            // Valida que duas chaves com mesmo texto são criadas diferente
            Assert.Equal(cryptedData, crypto2.Encrypt(message));
            
            var messageLong = "{\"ClientId\":1,\"DueDate\":\"2020 - 10 - 10T00: 00:00\",\"EmailLicenses\":1,\"PhoneLicenses\":0,\"ChatLicenses\":12,\"HasVirtualAssistant\":true}";
            var cryptedLongData = crypto.Encrypt(messageLong);
            Assert.Equal(messageLong, crypto.Decrypt(cryptedLongData));
        }
    }
}