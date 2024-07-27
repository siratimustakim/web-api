using System.Globalization;

namespace QuranApp.Util
{
    public class Utils
    {
        public static string GetArabicNumber(string input)
        {
            var culture = CultureInfo.GetCultureInfo("ar-SA");
            string[] digits = culture.NumberFormat.NativeDigits.Length >= 10
               ? culture.NumberFormat.NativeDigits
               : CultureInfo.InvariantCulture.NumberFormat.NativeDigits;

            return string.Concat(input
              .Select(c => char.IsDigit(c)
                 ? digits[(int)(char.GetNumericValue(c) + 0.5)]
                 : c.ToString()));
        }
    }
}