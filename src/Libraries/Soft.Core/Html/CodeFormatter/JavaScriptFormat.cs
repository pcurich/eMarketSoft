﻿namespace Soft.Core.Html.CodeFormatter
{
    public partial class JavaScriptFormat : CLikeFormat
    {
        /// <summary>
        /// Lista de JavaScript keywords
        /// </summary>
        protected override string Keywords
        {
            get
            {
                return "var function abstract as base bool break byte case catch char "
                       + "checked class const continue decimal default delegate do double else "
                       + "enum event explicit extern false finally fixed float for foreach goto "
                       + "if implicit in int interface internal is lock long namespace new null "
                       + "object operator out override params private protected public readonly "
                       + "ref return sbyte sealed short sizeof stackalloc static string struct "
                       + "switch this throw true try typeof uint ulong unchecked unsafe ushort "
                       + "using virtual void while";
            }
        }

        /// <summary>
        /// Uso de pre procesador color para markar directivas que deben 
        /// comenzar con @
        /// </summary>
        protected override string Preprocessors
        {
            get { return @"@\w*"; }
        }
    }
}