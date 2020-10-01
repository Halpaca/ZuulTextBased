using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulTextBased.Utility
{
    //TODO: add to narrator class
    internal static class Spelling
    {
        public static string AorAn(string nextword)
        {
            return AorAn(nextword.Substring(0, 1));
        }

        public static string AorAn(char c)
        {
            if(IsVowel(c))
            {
                return "an";
            }
            else
            {
                return "a";
            }
        }

        public static bool IsVowel(char c)
        {
            return "aeiou".IndexOf(c, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
