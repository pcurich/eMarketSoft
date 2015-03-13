namespace Soft.Core.Html.CodeFormatter
{
    /// <summary>
    /// Hndles para todas las opciones para el cambio
    /// del codigo renderizado
    /// </summary>
    public partial class HighlightOptions
    {
        public string Code { get; set; }
        public bool DisplayLineNumbers { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        public bool AlternateLineNumbers { get; set; }
    }
}