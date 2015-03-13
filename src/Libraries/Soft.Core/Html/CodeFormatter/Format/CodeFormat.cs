using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Soft.Core.Html.CodeFormatter.Format
{
    /// <summary>
    /// Provee una clase base para el formato de la mayoria de lenguajes de programacion
    /// </summary>
    public abstract partial class CodeFormat : SourceFormat
    {
        protected abstract string Keywords { get; }

        protected virtual string Preprocessors
        {
            get { return ""; }
        }

        protected abstract string StringRegex { get; }

        protected abstract string CommentRegex { get; }

        public virtual bool CaseSensitive
        {
            get { return true; }
        }

        protected CodeFormat()
        {
            var r = new Regex(@"\w+|-\w+|#\w+|@@\w+|#(?:\\(?:s|w)(?:\*|\+)?\w+)+|@\\w\*+");
            var regKeyword = r.Replace(Keywords, @"(?<=^|\W)$0(?=\W)");
            var regPreproc = r.Replace(Preprocessors, @"(?<=^|\s)$0(?=\s|$)");
            r = new Regex(@" +");
            regKeyword = r.Replace(regKeyword, @"|");
            regPreproc = r.Replace(regPreproc, @"|");

            if (regPreproc.Length == 0)
            {
                regPreproc = "(?!.*)_{37}(?<!.*)"; //use something quite impossible...
            }

            //build a master regex with capturing groups
            var regAll = new StringBuilder();
            regAll.Append("(");
            regAll.Append(CommentRegex);
            regAll.Append(")|(");
            regAll.Append(StringRegex);
            if (regPreproc.Length > 0)
            {
                regAll.Append(")|(");
                regAll.Append(regPreproc);
            }
            regAll.Append(")|(");
            regAll.Append(regKeyword);
            regAll.Append(")");

            var caseInsensitive = CaseSensitive ? 0 : RegexOptions.IgnoreCase;
            CodeRegex = new Regex(regAll.ToString(), RegexOptions.Singleline | caseInsensitive);
        }

        protected override string MatchEval(Match match)
        {
            if (match.Groups[1].Success) //comment
            {
                var reader = new StringReader(match.ToString());
                string line;
                var sb = new StringBuilder();
                while ((line = reader.ReadLine()) != null)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("\n");
                    }
                    sb.Append("<span class=\"rem\">");
                    sb.Append(line);
                    sb.Append("</span>");
                }
                return sb.ToString();
            }
            if (match.Groups[2].Success) //string literal
            {
                return "<span class=\"str\">" + match + "</span>";
            }
            if (match.Groups[3].Success) //preprocessor keyword
            {
                return "<span class=\"preproc\">" + match + "</span>";
            }
            if (match.Groups[4].Success) //keyword
            {
                return "<span class=\"kwrd\">" + match + "</span>";
            }
            System.Diagnostics.Debug.Assert(false, "None of the above!");
            return ""; //none of the above
        }
    }
}