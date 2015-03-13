using System;
using System.Text.RegularExpressions;
using Soft.Core.Html.CodeFormatter;

namespace Soft.Core.Html
{
    public class BbCodeHelper
    {
        #region Campos

        private static readonly Regex RegexBold = new Regex(@"\[b\](.+?)\[/b\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex RegexItalic = new Regex(@"\[i\](.+?)\[/i\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex RegexUnderLine = new Regex(@"\[u\](.+?)\[/u\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex RegexUrl1 = new Regex(@"\[url\=([^\]]+)\]([^\]]+)\[/url\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex RegexUrl2 = new Regex(@"\[url\](.+?)\[/url\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private static readonly Regex RegexQuote = new Regex(@"\[quote=(.+?)\](.+?)\[/quote\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Metodos

        public static string FormatText(string text, bool replaceBold, bool replaceItalic,
            bool replaceUnderline, bool replaceUrl, bool replaceCode, bool replaceQuote)
        {
            if (String.IsNullOrEmpty(text))
                return string.Empty;

            if (replaceBold)
            {
                // format the bold tags: [b][/b]
                // becomes: <strong></strong>
                text = RegexBold.Replace(text, "<strong>$1</strong>");
            }

            if (replaceItalic)
            {
                // format the italic tags: [i][/i]
                // becomes: <em></em>
                text = RegexItalic.Replace(text, "<em>$1</em>");
            }

            if (replaceUnderline)
            {
                // format the underline tags: [u][/u]
                // becomes: <u></u>
                text = RegexUnderLine.Replace(text, "<u>$1</u>");
            }

            if (replaceUrl)
            {
                // format the url tags: [url=http://www.nopCommerce.com]my site[/url]
                // becomes: <a href="http://www.nopCommerce.com">my site</a>
                text = RegexUrl1.Replace(text, "<a href=\"$1\" rel=\"nofollow\">$2</a>");

                // format the url tags: [url]http://www.nopCommerce.com[/url]
                // becomes: <a href="http://www.nopCommerce.com">http://www.nopCommerce.com</a>
                text = RegexUrl2.Replace(text, "<a href=\"$1\" rel=\"nofollow\">$1</a>");
            }

            if (replaceQuote)
            {
                while (RegexQuote.IsMatch(text))
                    text = RegexQuote.Replace(text, "<b>$1 wrote:</b><div class=\"quote\">$2</div>");
            }

            if (replaceCode)
            {
                text = CodeFormatHelper.FormatTextSimple(text);
            }

            return text;
        }

        public static string RemoveQuotes(string str)
        {
            str = Regex.Replace(str, @"\[quote=(.+?)\]", String.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            str = Regex.Replace(str, @"\[/quote\]", String.Empty, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return str;
        }

        #endregion
    }
}