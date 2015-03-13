using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Soft.Core.Html.CodeFormatter.Format
{
    public abstract partial class SourceFormat
    {
        protected SourceFormat()
        {
            TabSpaces = 4;
            LineNumbers = false;
            Alternate = false;
            EmbedStyleSheet = false;
        }

        //Tamaño de los tabs
        public byte TabSpaces { get; set; }

        //Numero de lineas de salida
        public bool LineNumbers { get; set; }

        //Activa o desactiva las lineas de fondo
        public bool Alternate { get; set; }

        //Hojas embebidas de CSS
        public bool EmbedStyleSheet { get; set; }

        /// <summary>
        /// Expresiones regulares uzadas como token 
        /// </summary>
        public Regex CodeRegex { get; set; }

        /// <summary>
        /// Transforma source code a HTML 4.01
        /// </summary>
        /// <param name="source"></param>
        /// <returns>una cadena con formato Html</returns>
        public string FormatCode(Stream source)
        {
            var reader = new StreamReader(source);
            string s = reader.ReadToEnd();
            reader.Close();
            return FormatCode(s, LineNumbers, Alternate, EmbedStyleSheet, false);
        }

        public string FormatCode(string source)
        {
            return FormatCode(source, LineNumbers, Alternate, EmbedStyleSheet, false);
        }

        public string FormatSubCode(string source)
        {
            return FormatCode(source, false, false, false, true);
        }

        /// <summary>
        /// Obtiene el CSS stylesheet como un stream
        /// </summary>
        /// <returns></returns>
        public static Stream GetCssStream()
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(
                "Manoli.Utils.CSharpFormat.csharp.css");
        }

        /// <summary>
        /// Obtiene el CSS stylesheet como un string
        /// </summary>
        /// <returns></returns>
        public static string GetCssString()
        {
            var reader = new StreamReader(GetCssStream());
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Llamado para evaluar los fragmentos de Html que pertenecen 
        /// a cada token que matching en el code
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        protected abstract string MatchEval(Match match);

        private string FormatCode(string source, bool lineNumbers,
            bool alternate, bool embedStyleSheet, bool subCode)
        {
            //replace special characters
            var sb = new StringBuilder(source);

            if (!subCode)
            {
                sb.Replace("&", "&amp;");
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                sb.Replace("\t", string.Empty.PadRight(TabSpaces));
            }

            //color the code
            source = CodeRegex.Replace(sb.ToString(), MatchEval);

            sb = new StringBuilder();

            if (embedStyleSheet)
            {
                sb.Append("<style type=\"text/css\">\n");
                sb.Append(GetCssString());
                sb.Append("</style>\n");
            }

            if (lineNumbers || alternate) //Tenemos que procesar la linea de codigo
            {
                if (!subCode)
                    sb.Append("<div class=\"csharpcode\">\n");

                var reader = new StringReader(source);
                var i = 0;
                const string spaces = "    ";
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    i++;
                    if (alternate && ((i%2) == 1))
                    {
                        sb.Append("<pre class=\"alt\">");
                    }
                    else
                    {
                        sb.Append("<pre>");
                    }

                    if (lineNumbers)
                    {
                        var order = (int) Math.Log10(i);
                        sb.Append("<span class=\"lnum\">"
                                  + spaces.Substring(0, 3 - order) + i
                                  + ":  </span>");
                    }

                    sb.Append(line.Length == 0 ? "&nbsp;" : line);
                    sb.Append("</pre>\n");
                }
                reader.Close();

                if (!subCode)
                    sb.Append("</div>");
            }
            else
            {
                //have to use a <pre> because IE below ver 6 does not understand 
                //the "white-space: pre" CSS value
                if (!subCode)
                    sb.Append("<pre class=\"csharpcode\">\n");
                sb.Append(source);
                if (!subCode)
                    sb.Append("</pre>");
            }

            return sb.ToString();
        }
    }
}