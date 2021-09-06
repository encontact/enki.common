﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace enki.common.core
{
    public static class StringUtils
    {
        public const string EmailRegExp = @"(^([\w\+\=\-\.\&\u00C0-\u00ff]?)+[\u00C0-\u00ffa-zA-Z0-9_-]@[\u00C0-\u00ffa-zA-Z0-9]{1}(?:[\u00C0-\u00ffa-zA-Z0-9\-]*?|[\.]{1})[\u00C0-\u00ffa-zA-Z0-9]{1,}(?:\.{1}[\u00C0-\u00ffa-zA-Z0-9\-]{1,})+?$)|^([^<>]*?)<(\s*([\w\+\=\-\.\&\u00C0-\u00ff]?)+[\u00C0-\u00ffa-zA-Z0-9_-]@[\u00C0-\u00ffa-zA-Z0-9]{1}(?:[\u00C0-\u00ffa-zA-Z0-9\-]*?|[\.]{1})[\u00C0-\u00ffa-zA-Z0-9]{1,}(?:\.{1}[\u00C0-\u00ffa-zA-Z0-9\-]{1,})+?\s*)>$";

        /// <summary>
        /// Formata Strings de acordo com o informado. Ex: ##/##/#### ou ##.###,##
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="mascara"></param>
        /// <returns></returns>
        public static string format(string valor, string mascara)
        {
            StringBuilder dado = new StringBuilder();
            // remove caracteres nao numericos
            foreach (char c in valor)
            {
                if (char.IsNumber(c))
                    dado.Append(c);
            }
            int indMascara = mascara.Length;
            int indCampo = dado.Length;
            for (; indCampo > 0 && indMascara > 0;)
            {
                if (mascara[--indMascara] == '#')
                    indCampo--;
            }
            StringBuilder saida = new StringBuilder();
            for (; indMascara < mascara.Length; indMascara++)
            {
                saida.Append((mascara[indMascara] == '#') ? dado[indCampo++] : mascara[indMascara]);
            }
            return saida.ToString();
        }

        public static byte[] GetBytes(string message)
        {
            byte[] bytes = new byte[message.Length * sizeof(char)];
            System.Buffer.BlockCopy(message.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        public static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        /// <summary>
        /// Encoda uma string qualquer em BASE64
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        /// <summary>
        /// Decoda uma string em BASE64 para a String original
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string base64Decode(string data)
        {
            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }

        /// <summary>
        /// Recupera o e-mail contido num texto, por exemplo "Reinaldo Coelho Sartorelli <reinaldo@enkiconsultoira.com.br>" retorna "reinaldo@enkiconsultoria.com.br"
        /// </summary>
        /// <param name="text">Texto a ser processado.</param>
        /// <returns>Email encontrado.</returns>
        public static string ExtractEmailAddress(string text)
        {
            string emailTo = text.Trim();
            var emailGroups = Regex.Match(emailTo.ToLower(), EmailRegExp).Groups;
            var emailAddress = !string.IsNullOrEmpty(emailGroups[1]?.Value) ? emailGroups[1]?.Value : emailGroups[4]?.Value;
            return emailAddress.Trim();
        }

        /// <summary>
        /// Recupera o nome contido num texto de email, por exemplo "Reinaldo Coelho Sartorelli <reinaldo@enkiconsultoira.com.br>" retorna "Reinaldo Coelho Sartorelli"
        /// </summary>
        /// <param name="text">Texto a ser processado.</param>
        /// <returns>Nome encontrado.</returns>
        public static string ExtractEmailName(string text)
        {
            var emailTo = text.Trim();
            var emailGroups = Regex.Match(emailTo, EmailRegExp).Groups;
            var name = (emailGroups[3]?.Value ?? "").Trim();

            if (name == "") return name;
            if (name.Length <= 2) return name;
            var firstChar = name.Substring(0, 1);
            var lastChar = name.Substring(name.Length - 1, 1);
            var toRemoveCharList = new List<string> { "\"", "'", "`", "´" };
            if (toRemoveCharList.Contains(firstChar) && firstChar == lastChar)
            {
                name = name.Substring(0, name.Length - 1).Substring(1);
            }

            return name.Trim();
        }

        // /// <summary>
        // /// Remove acentos de uma string qualquer.
        // /// </summary>
        // /// <param name="text">Texto para remover os acentos.</param>
        // /// <returns>Mesma string sem os acentos.</returns>
        /// Proposta no Twitter do Lucas Teles para esta função: https://twitter.com/LucasTeles42/status/1319070078126739457
        /// Códigos: https://sharplab.io/#v2:EYLgxg9gTgpgtADwGwBYA0ATEBqAPgAQCYBGAWACh8AGAAn2IDoBxAGwmAEMWBLALw4Au3CADsA3BWp1GAFRgIBEyrXoMAMtxEBHJRREcAtjADOABw5gYNABYwWbAPphjFAN4UanuoWkB2D17u5AFeXsYCUACuYAI0AGIQEDRBoale+ADMNJqxABocwCwckUppngC+FCGp9Eh0KDQAshyaABT0VADaALo0HFAA5sYAlNWhQWNlAPQAVJNpkSLGHABmVvgNAMIQBqaRAjCtOTM0phGj5GWpKVdlM2dQNAC8NK335zQnxFSltxVVlz+NEWyzWyXmtwAbv1ss8aN8fhCrsdThE4QAybi/IFeJFlba7faHTHDXSAnGeegATlaABIAEQyWw0aEsSJWCArGgCZnQqDcAosKzcbLGGggZLccr00l4tJTKY0Rm8rjsmic7kq/mC4Wi8XwqiGqj0uU0Srk1IzKYQ6mtABKMAMEEhMAAgpYRAIIMZWvSAEfGACXGAA5ysZbKLaFbQ6nS73TBPd6NNpfQHg2GI2SruaAVdatJaLHnW6PV6fR1ufIBMMaBCbn8+TQRNADFw+DAMABlCKaAZwg4KBgAOVb7d4h1HUDbPH4QlECWnDEXBgAIpGcU2OgAhSLcFgYGCPF4iGAAdxoPf5IgGu/3h6grUjppW0BgFmsrybYGyImbY9nTsrz7C4cQbCkm0WbhIEPTZBBgAZoAATzhTZrH6ABVERoIgQ8AEkRFfZgYAELCcNg+DEKgJDWjADcKU8bguVaKCYJgOCDiolCAEIXjItiOIQ5CR1ELtzDAPtmigABrUCGJqb47wPI8GFdUxTETDBaPov5cyjMp8F8QslIfBgZAgYCbyfETp3HScAL4QRhBEFdl1bTYdNSc0/htYg6krYt4zLZNNC0dpvirBRayeAA+Ot9LKQcBFNTwbJnDtWindK52c1yV3XFKaAYAB1WxYFo544rQzDsLYgiiKYEj+Nw9jKOQ7T4vkileJoZqKM44TRxEMSLEk/pZMK1SBgGWABng316TQV4LDANA6MqvowB/bAaDoyasrszKHJyhdWzc6cPOxLyAT0iggA
        // public static string RemoveAccents(string text)
        // {
        //     try
        //     {
        //         StringBuilder sbReturn = new StringBuilder();
        //         var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

        //         foreach (char letter in arrayText)
        //         {
        //             if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
        //                 sbReturn.Append(letter);
        //         }
        //         return sbReturn.ToString();
        //     }
        //     catch
        //     {
        //         throw;
        //     }
        // }

        /// <summary>
        /// Remove qualquer caractere do texto que não seja um texto de A-Z/a-z ou numérico de 0-9
        /// </summary>
        /// <param name="text">Texto original</param>
        /// <returns>Texto tratado</returns>
        public static string GetOnlyLettersAndNumbers(string text)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            return rgx.Replace(text, "");
        }

        /// <summary>
        /// A partir de um texto representando um valor monetário, exemplo 99.655,00 ou 99,655.00 ou 99.5, recupera o Double equivalente.
        /// </summary>
        /// <param name="text">Texto representando o valor monetário que se quer transformar</param>
        /// <param name="DecimalSeparator">Separador de casa decimal a ser utilizada.</param>
        /// <param name="GroupSeparator">Separador de grupo numério.</param>
        /// <returns>Double equivalente ao valor do texto.</returns>
        public static double CurrencyFrom(string text, string DecimalSeparator = ",", string GroupSeparator = ".")
        {
            var numberFormat = new NumberFormatInfo();
            numberFormat.NumberDecimalSeparator = DecimalSeparator;
            numberFormat.NumberGroupSeparator = GroupSeparator;

            var originalText = text;
            text = "000" + text.Replace("R$", "").Trim();
            // Processa logica de conversão
            var finalText = "";
            var decimalPoint = text.Substring(text.Length - 3, 1);
            if (decimalPoint != "." && decimalPoint != ",")
            {
                var lastPointIndex = text.Substring(text.Length - 3).LastIndexOf('.');
                var lastCommaIndex = text.Substring(text.Length - 3).LastIndexOf(',');
                if (lastPointIndex == -1 && lastCommaIndex == -1) decimalPoint = "";
                else if ((lastCommaIndex != -1 && lastPointIndex != -1) && (lastCommaIndex > lastPointIndex)) decimalPoint = ",";
                else if ((lastCommaIndex != -1 && lastPointIndex != -1) && (lastPointIndex > lastCommaIndex)) decimalPoint = ".";
                else if (lastCommaIndex != -1 && lastPointIndex == -1) decimalPoint = ",";
                else if (lastCommaIndex == -1 && lastPointIndex != -1) decimalPoint = ".";
            }
            switch (decimalPoint)
            {
                case ".":
                    finalText = text.Replace(",", "").Replace(".", ",");
                    break;

                case ",":
                    finalText = text.Replace(".", "");
                    break;

                default:
                    finalText = text.Replace(".", "").Replace(",", "");
                    break;
            }
            var result = 0.00;
            if (!Double.TryParse(finalText, out result))
            {
                throw new System.InvalidOperationException("O valor " + originalText + " não pode ser convertido para double.");
            }
            return Convert.ToDouble(finalText, numberFormat);
        }
    }
}