using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace ReplaceChars
{
    static class Program
    {
        static void Main(string[] args)
        {
            while (true) { 
                Console.Write("Enter Text: ");
                string inputText = Console.ReadLine();

            }
        }

        [Pure]
        public static string StripDiacritics(this string s)
        {
            if (s == null) throw new ArgumentNullException("s");

            s = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder(s.Length);

            for (int i = 0; i < s.Length; ++i)
            {
                if (char.GetUnicodeCategory(s, i) != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    if (!char.IsSurrogatePair(s, i))
                    {
                        sb.Append(s[i]);
                    }
                    else
                    {
                        sb.Append(s, i, 2);
                        ++i;
                    }
                }
            }
            return sb.ToString().Normalize();
        }
    }
}
