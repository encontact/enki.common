using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace enki.common.core.WebUtils
{
    public class TextToHtml
    {
        public string OriginalText { get; private set; }

        public TextToHtml(string html)
        {
            OriginalText = html;
        }

        /// <summary>
        /// Recupera o texto
        /// </summary>
        /// <returns>Texto encodado em tml.</returns>
        public string GetHtml()
        {
            var body = HttpUtility.HtmlEncode(OriginalText).Replace("\r\n", "<br>");
            // Substitui caso de quebra de linha gerada por Bug no notepad.exe
            // Referências: 
            // http://stackoverflow.com/questions/6998506/text-file-with-0d-0d-0a-line-breaks
            // http://bavih.blogspot.com.br/2008/07/notepad-bug.html
            body = body.Replace("\r", "<br>");
            body = body.Replace("\n", "<br>");
            return string.Concat("<p>", body, "</p>");
        }
    }
}