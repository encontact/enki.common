using Xunit;

namespace enki.common.core.Tests
{
    public class StringUtilsTests
    {
        [Theory]
        [InlineData("reinaldo@enki.com", "\"Reinaldo C Sartorelli\" <reinaldo@enki.com>")]
        [InlineData("emailsimples@hotmail.com", "emailsimples@hotmail.com")]
        [InlineData("simplescomsinais@hotmail.com", "<simplescomsinais@hotmail.com>")]
        [InlineData("nomecomprido@enkilabs.com.br", "nomecomprido Coelho Sartorelli <   nomecomprido@enkilabs.com.br >")]
        [InlineData("nomecomprido@enkilabs.com.br", "'nomecomprido Coelho Sartorelli' <   nomecomprido@enkilabs.com.br >")]
        [InlineData("nomecomprido@enkilabs.com.br", "`nomecomprido Coelho Sartorelli` <   nomecomprido@enkilabs.com.br >")]
        [InlineData("nomecomprido@enkilabs.com.br", "´nomecomprido Coelho Sartorelli´ <   nomecomprido@enkilabs.com.br >")]
        public void TestExtractEmailAddress(string expected, string originalData) => Assert.Equal(expected, StringUtils.ExtractEmailAddress(originalData));

        [Theory]
        [InlineData("Reinaldo C Sartorelli", "\"Reinaldo C Sartorelli\" <reinaldo@enki.com>")]
        [InlineData("Reinaldo C Sartorelli", "Reinaldo C Sartorelli <reinaldo@enki.com>")]
        [InlineData("imesmo inicio e fimi", "imesmo inicio e fimi <reinaldo@enki.com>")]
        [InlineData("", "emailsimples@hotmail.com")]
        [InlineData("", "<simplescomsinais@hotmail.com>")]
        [InlineData("nomecomprido Coelho Sartorelli", "nomecomprido Coelho Sartorelli <   nomecomprido@enkilabs.com.br >")]
        [InlineData("nomecomprido Coelho Sartorelli", "'nomecomprido Coelho Sartorelli' <   nomecomprido@enkilabs.com.br >")]
        [InlineData("nomecomprido Coelho Sartorelli", "`nomecomprido Coelho Sartorelli` <   nomecomprido@enkilabs.com.br >")]
        [InlineData("nomecomprido Coelho Sartorelli", "´nomecomprido Coelho Sartorelli´ <   nomecomprido@enkilabs.com.br >")]
        public void TestExtractEmailName(string expected, string originalData) => Assert.Equal(expected, StringUtils.ExtractEmailName(originalData));
    }
}