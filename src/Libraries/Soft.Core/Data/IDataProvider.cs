using System.Data.Common;

namespace Soft.Core.Data
{
    /// <summary>
    /// Proveedor de la data
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Inicializa las conexiones
        /// </summary>
        void InitConnectionFactory();

        /// <summary>
        /// Establece el inicio de la base de datos
        /// </summary>
        void SetDatabaseInitializer();

        /// <summary>
        /// Inicializa la base de datos
        /// </summary>
        void InitDatabase();

        /// <summary>
        /// Indica si el proveedor de base de datos soporta storeProcedures0
        /// </summary>
        bool StoredProceduredSupported { get; }

        /// <summary>
        /// Establece el soporte para los parametros de la base de datos
        /// (solo se usan en storeProcedure)
        /// </summary>
        /// <returns></returns>
        DbParameter GetParameter();
    }
}