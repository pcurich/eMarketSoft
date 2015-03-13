using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Soft.Core.Html
{
    public class ResolveLinksHelper
    {
        #region Campos

        /// <summary>
        /// Expresiones regulares usadas para parsear a links
        /// </summary>
        private static readonly Regex Regex =
            new Regex("((http://|https://|www\\.)([A-Z0-9.\\-]{1,})\\.[0-9A-Z?;~&\\(\\)#,=\\-_\\./\\+]{2,})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private const string Link = "<a href=\"{0}{1}\" rel=\"nofollow\">{2}</a>";
        private const int MaxLength = 50;

        #endregion

        #region Util

        private static string ShortenUrl(string url, int max)
        {
            if (url.Length <= max)
                return url;

            // Remove the protocal
            var startIndex = url.IndexOf("://", StringComparison.Ordinal);
            if (startIndex > -1)
                url = url.Substring(startIndex + 3);

            if (url.Length <= max)
                return url;

            // Compress folder structure
            var firstIndex = url.IndexOf("/", StringComparison.Ordinal) + 1;
            var lastIndex = url.LastIndexOf("/", StringComparison.Ordinal);
            if (firstIndex < lastIndex)
            {
                url = url.Remove(firstIndex, lastIndex - firstIndex);
                url = url.Insert(firstIndex, "...");
            }

            if (url.Length <= max)
                return url;

            // Remove URL parameters
            var queryIndex = url.IndexOf("?", StringComparison.Ordinal);
            if (queryIndex > -1)
                url = url.Substring(0, queryIndex);

            if (url.Length <= max)
                return url;

            // Remove URL fragment
            var fragmentIndex = url.IndexOf("#", StringComparison.Ordinal);
            if (fragmentIndex > -1)
                url = url.Substring(0, fragmentIndex);

            if (url.Length <= max)
                return url;

            // Compress page
            firstIndex = url.LastIndexOf("/", StringComparison.Ordinal) + 1;
            lastIndex = url.LastIndexOf(".", StringComparison.Ordinal);
            if (lastIndex - firstIndex > 10)
            {
                var page = url.Substring(firstIndex, lastIndex - firstIndex);
                var length = url.Length - max + 3;
                if (page.Length > length)
                    url = url.Replace(page, "..." + page.Substring(length));
            }

            return url;
        }

        #endregion

        #region Metodo

        public static string FormatText(string text)
        {
            if (String.IsNullOrEmpty(text))
                return string.Empty;

            var info = CultureInfo.InvariantCulture;
            var str = String.Empty;

            foreach (Match match in Regex.Matches(text))
            {
                str = String.Empty;
                if (!match.Value.Contains("://"))
                {
                    str = "http://";
                }

                text = text.Replace(match.Value,
                    string.Format(info, Link, str, match.Value, ShortenUrl(match.Value, MaxLength)));
            }

            return text;
        }

        #endregion
    }
}