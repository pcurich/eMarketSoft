namespace Soft.Core.Domain.Catalog
{
    /// <summary>
    /// Representa un control de typos de los atributos
    /// </summary>
    public enum AttributeControlType
    {
        /// <summary>
        /// Dropdown lista
        /// </summary>
        DropdownList = 1,

        /// <summary>
        /// Radio lista
        /// </summary>
        RadioList = 2,

        /// <summary>
        /// Checkboxes
        /// </summary>
        Checkboxes = 3,

        /// <summary>
        /// TextBox
        /// </summary>
        TextBox = 4,

        /// <summary>
        /// Multiple textbox
        /// </summary>
        MultilineTextbox = 10,

        /// <summary>
        /// Datepicker
        /// </summary>
        Datepicker = 20,

        /// <summary>
        /// File upload control
        /// </summary>
        FileUpload = 30,

        /// <summary>
        /// Color squares
        /// </summary>
        ColorSquares = 40,

        /// <summary>
        /// Read-only checkboxes
        /// </summary>
        ReadonlyCheckboxes = 50,
    }
}