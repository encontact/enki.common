using System.Text;
using Xunit;

namespace enki.common.core.WebUtils.Tests
{
    public class TextToHtmlTests
    {
        [Theory]
        [InlineData("Texto simples, sem quebra linha", "<p>Texto simples, sem quebra linha</p>")]
        [InlineData("Texto simples, \r\ncom quebra linha", "<p>Texto simples, <br>com quebra linha</p>")]
        [InlineData("Texto simples, \ncom quebra linha", "<p>Texto simples, <br>com quebra linha</p>")]
        [InlineData("Texto simples, \rcom quebra linha", "<p>Texto simples, <br>com quebra linha</p>")]
        [InlineData("Texto com acentuação para avaliação.", "<p>Texto com acentua&#231;&#227;o para avalia&#231;&#227;o.</p>")]
        public void TestHtmlGetAttributeValueToSrcAttributeOk(string text, string expectedResult)
        {
            var processor = new TextToHtml(text);
            Assert.Equal(expectedResult, processor.GetHtml());
        }
    }
}

