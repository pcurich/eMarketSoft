//Contributor:  Nicholas Mayne

using System.Collections.Generic;
using Soft.Core.Domain.Customers;

namespace Soft.Services.Authentication.External
{
    /// <summary>
    /// servicio para abrir una autenticacion 
    /// </summary>
    public partial interface IOpenAuthenticationService
    {
        /// <summary>
        /// Cargar métodos activos de autenticación externos
        /// </summary>
        /// <param name="storeId">Carga registros para una tienda especifica; 0 carga todas</param>
        /// <returns>Metodos de pagos</returns>
        IList<IExternalAuthenticationMethod> LoadActiveExternalAuthenticationMethods(int storeId = 0);

        /// <summary>
        /// Carga metodos de autenticacion externa por el nombre del sistemas
        /// </summary>
        /// <param name="systemName">Nombre del sistema</param>
        /// <returns>Encontrar metodo de autenticacion externa.</returns>
        IExternalAuthenticationMethod LoadExternalAuthenticationMethodBySystemName(string systemName);

        /// <summary>
        /// Carga todos los metodos de autenticacion externa
        /// </summary>
        /// <param name="storeId">Carga registros para una tienda especifica; 0 carga todas</param>
        /// <returns>External authentication methods</returns>
        IList<IExternalAuthenticationMethod> LoadAllExternalAuthenticationMethods(int storeId = 0);

        /// <summary>
        /// Si exixte la cuenta
        /// </summary>
        /// <param name="parameters">parametros</param>
        /// <returns></returns>
        bool AccountExists(OpenAuthenticationParameters parameters);

        /// <summary>
        /// Asociar una cuenta externa con un usuario
        /// </summary>
        /// <param name="customer">Clientes</param>
        /// <param name="parameters">parametros</param>
        void AssociateExternalAccountWithUser(Customer customer, OpenAuthenticationParameters parameters);

        /// <summary>
        /// Optiene el usario desde los parametros de autenticacion
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Customer GetUser(OpenAuthenticationParameters parameters);

        /// <summary>
        /// Retorna una lista de registros de autenticacion externa para un cliente 
        /// </summary>
        /// <param name="customer">Cliente</param>
        /// <returns></returns>
        IList<ExternalAuthenticationRecord> GetExternalIdentifiersFor(Customer customer);

        /// <summary>
        /// Borra el registro de autenticacion externa
        /// </summary>
        /// <param name="externalAuthenticationRecord">Registro de autenticacion externa</param>
        void DeletExternalAuthenticationRecord(ExternalAuthenticationRecord externalAuthenticationRecord);

        /// <summary>
        /// Remueve la asociacion con el cliente
        /// </summary>
        /// <param name="parameters"></param>
        void RemoveAssociation(OpenAuthenticationParameters parameters);
    }
}