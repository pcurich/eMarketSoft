using Soft.Core.Domain.Localization;

namespace Soft.Core.Domain.Configuration
{
    /// <summary>
    /// Representa una configuracion
    /// </summary>
    public partial class Setting : BaseEntity, ILocalizedEntity
    {
        public Setting()
        {
        }

        public Setting(string name, string value, int storeId = 0)
        {
            Name = name;
            Value = value;
            StoreId = storeId;
        }

        /// <summary>
        /// Nombrede la configuracion
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Valor de la configuracion
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Se aplica a determinada tienda si es cero se aplica a todas las tiendas
        /// </summary>
        public int StoreId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}