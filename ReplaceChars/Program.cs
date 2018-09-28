using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace ReplaceChars
{
    static class Program
    {
        static void Main(string[] args)
        {
            while (true) { 
                Console.Write("Enter Text: ");
                string inputText = Console.ReadLine();
                Console.WriteLine("Output: " + SafeEncodeString(inputText));
            }
        }

        public static string SafeEncodeString(string encodeString)
        {
            if (encodeString == null)
                return null;

            encodeString = StripDiacritics(encodeString);

            // this misses some, so here's a few other replacements.
            encodeString = encodeString.Replace("\u00E6", "ae");
            encodeString = encodeString.Replace("\u00C6", "AE");
            encodeString = encodeString.Replace("\u00DF", "ss");
            encodeString = encodeString.Replace("ß", "ss");
            encodeString = encodeString.Replace("’", "'");
            encodeString = encodeString.Replace("é", "e");

            // replace a few other Unicode characters as needed
            encodeString = encodeString.Replace("\u00A9", "(C)");
            encodeString = encodeString.Replace("\u00AE", "(R)");
            encodeString = encodeString.Replace("\u2122", "(tm)");

            // lastly, remove any and all unicode characters that are still lingering about.
            string.Concat(encodeString.Normalize(NormalizationForm.FormD).Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));

            return encodeString;
        }
        
        public static string StripDiacritics(this string InputString)
        {
            if (InputString == null) return null;

            InputString = InputString.Normalize(NormalizationForm.FormD);
            StringBuilder outputString = new StringBuilder(InputString.Length);

            for (int i = 0; i < InputString.Length; ++i)
            {
                if (char.GetUnicodeCategory(InputString, i) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    if (!char.IsSurrogatePair(InputString, i))
                    {
                        outputString.Append(InputString[i]);
                    }
                    else
                    {
                        outputString.Append(InputString, i, 2);
                        ++i;
                    }
                }
            }
            return outputString.ToString().Normalize();
        }
    }
}
