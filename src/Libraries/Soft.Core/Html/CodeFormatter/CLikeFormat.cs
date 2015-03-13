using Soft.Core.Html.CodeFormatter.Format;

namespace Soft.Core.Html.CodeFormatter
{
    /// <summary>
    /// Provee una base para el formato del lenguaje C
    /// </summary>
    public abstract partial class CLikeFormat : CodeFormat
    {
        /// <summary>
        /// Expresiones regulares de cadenas que igualan linea simples y 
        /// multileneas de comentarios (// y /* */)
        /// </summary>
        protected override string CommentRegex
        {
            get { return @"/\*.*?\*/|//.*?(?=\r|\n)"; }
        }

        /// <summary>
        /// Expresiones regulares de cadenas que igualan a caracteres lineales
        /// </summary>
        protected override string StringRegex
        {
            get { return @"@?""""|@?"".*?(?!\\).""|''|'.*?(?!\\).'"; }
        }
    }
}