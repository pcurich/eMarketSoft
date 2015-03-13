namespace Soft.Core.Domain.Logging
{
    /// <summary>
    /// Representa una actividad de typo log
    /// </summary>
    public class ActivityLogType : BaseEntity
    {
        /// <summary>
        ///´Palabras claves del sistema
        /// </summary>
        public string SystemKeyword { get; set; }

        /// <summary>
        /// Nombre a mostrar
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Que actividad de tipo log esta activa
        /// </summary>
        public bool Enabled { get; set; }
    }
}