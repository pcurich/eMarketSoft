using System;
using System.Text.RegularExpressions;
using System.Web;
using Soft.Core.Html.CodeFormatter.Format;

namespace Soft.Core.Html.CodeFormatter
{
    public partial class CodeFormatHelper
    {
        #region Campos

        private static readonly Regex RegexHtml = new Regex("<[^>]*>", RegexOptions.Compiled);

        private static readonly Regex RegexCode2 = new Regex(@"\[code\](?<inner>(.*?))\[/code\]",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Util

        private static string CodeEvaluator(Match match)
        {
            if (!match.Success)
                return match.Value;

            var options = new HighlightOptions
            {
                Language = match.Groups["lang"].Value,
                Code = match.Groups["code"].Value,
                DisplayLineNumbers = match.Groups["linenumbers"].Value == "on",
                Title = match.Groups["title"].Value,
                AlternateLineNumbers = match.Groups["altlinenumbers"].Value == "on"
            };

            var result = match.Value.Replace(match.Groups["begin"].Value, "");
            result = result.Replace(match.Groups["end"].Value, "");
            result = Highlight(options, result);
            return result;
        }

        private static string CodeEvaluatorSimple(Match match)
        {
            if (!match.Success)
                return match.Value;

            var options = new HighlightOptions
            {
                Language = "c#",
                Code = match.Groups["inner"].Value,
                DisplayLineNumbers = false,
                Title = string.Empty,
                AlternateLineNumbers = false
            };

            var result = match.Value;
            result = Highlight(options, result);
            return result;
        }

        /// <summary>
        /// Retorna el formato del texto
        /// </summary>
        /// <param name="options"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string Highlight(HighlightOptions options, string text)
        {
            switch (options.Language)
            {
                case "c#":
                    var csf = new CSharpFormat
                    {
                        LineNumbers = options.DisplayLineNumbers,
                        Alternate = options.AlternateLineNumbers
                    };
                    return HttpUtility.HtmlDecode(csf.FormatCode(text));

                case "vb":
                    var vbf = new VisualBasicFormat
                    {
                        LineNumbers = options.DisplayLineNumbers,
                        Alternate = options.AlternateLineNumbers
                    };
                    return vbf.FormatCode(text);

                case "js":
                    var jsf = new JavaScriptFormat
                    {
                        LineNumbers = options.DisplayLineNumbers,
                        Alternate = options.AlternateLineNumbers
                    };
                    return HttpUtility.HtmlDecode(jsf.FormatCode(text));

                case "html":
                    var htmlf = new HtmlFormat
                    {
                        LineNumbers = options.DisplayLineNumbers,
                        Alternate = options.AlternateLineNumbers
                    };
                    text = StripHtml(text).Trim();
                    var code = htmlf.FormatCode(HttpUtility.HtmlDecode(text)).Trim();
                    return code.Replace("\r\n", "<br />").Replace("\n", "<br />");

                case "xml":
                    var xmlf = new HtmlFormat
                    {
                        LineNumbers = options.DisplayLineNumbers,
                        Alternate = options.AlternateLineNumbers
                    };
                    text = text.Replace("<br />", "\r\n");
                    text = StripHtml(text).Trim();
                    var xml = xmlf.FormatCode(HttpUtility.HtmlDecode(text)).Trim();
                    return xml.Replace("\r\n", "<br />").Replace("\n", "<br />");

                case "tsql":
                    var tsqlf = new TsqlFormat
                    {
                        LineNumbers = options.DisplayLineNumbers,
                        Alternate = options.AlternateLineNumbers
                    };
                    return HttpUtility.HtmlDecode(tsqlf.FormatCode(text));

                case "msh":
                    var mshf = new MshFormat
                    {
                        LineNumbers = options.DisplayLineNumbers,
                        Alternate = options.AlternateLineNumbers
                    };
                    return HttpUtility.HtmlDecode(mshf.FormatCode(text));
            }

            return string.Empty;
        }

        /// <summary>
        /// Tiras de Html
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static string StripHtml(string html)
        {
            return string.IsNullOrEmpty(html)
                ? string.Empty
                : RegexHtml.Replace(html, string.Empty);
        }

        #endregion

        #region Metodo

        /// <summary>
        /// Formato de texto
        /// </summary>
        /// <param name="text"> </param>
        /// <returns></returns>
        public static string FormatTextSimple(string text)
        {
            if (String.IsNullOrEmpty(text))
                return string.Empty;

            if (!text.Contains("[/code]"))
                return text;

            text = RegexCode2.Replace(text, CodeEvaluatorSimple);
            text = RegexCode2.Replace(text, "$1");
            return text;
        }

        #endregion
    }
}