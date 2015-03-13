using System.Collections.Generic;
using Soft.Core;
using Soft.Core.Domain.Customers;
using Soft.Core.Domain.Security;

namespace Soft.Services.Security
{
    /// <summary>
    /// Interfaz para el ACL
    /// </summary>
    public partial interface IAclService
    {
        /// <summary>
        /// Borra un ACL Record
        /// </summary>
        /// <param name="aclRecord"></param>
        void DeleteAclRecord(AclRecord aclRecord);

        /// <summary>
        /// Obtiene un registro ACL 
        /// </summary>
        /// <param name="aclRecordId"></param>
        /// <returns></returns>
        AclRecord GetAclRecordById(int aclRecordId);

        /// <summary>
        /// Optiene un registro ACL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        IList<AclRecord> GetAclRecords<T>(T entity) where T : BaseEntity, IAclSupported;

        /// <summary>
        /// Inserta un registro ACL
        /// </summary>
        /// <param name="aclRecord"></param>
        void InsertAclRecord(AclRecord aclRecord);

        /// <summary>
        /// Inserta un registro ACL
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="entity">Id del rol del consumidor</param>
        /// <param name="customerRoleId">Entity</param>
        void InsertAclRecord<T>(T entity, int customerRoleId) where T : BaseEntity, IAclSupported;

        /// <summary>
        /// Actualiza un registro ACL
        /// </summary>
        /// <param name="aclRecord"></param>
        void UpdateAclRecord(AclRecord aclRecord);

        /// <summary>
        /// Encuentra los roles de un consumidor identificados con accesos
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int[] GetCustomerRoleIdsWithAccess<T>(T entity) where T : BaseEntity, IAclSupported;

        /// <summary>
        /// Permiso de autenticacion ACL
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="entity">Entity</param>
        /// <returns>true - Autorizado; otro false</returns>
        bool Authorize<T>(T entity) where T : BaseEntity, IAclSupported;

        /// <summary>
        /// Permiso de autenticacion ACL
        /// </summary>
        /// <typeparam name="T">Tipo</typeparam>
        /// <param name="entity">Entity</param>
        /// <param name="customer">consumidor</param>
        /// <returns>true - Autorizado; otro false</returns>
        bool Authorize<T>(T entity, Customer customer) where T : BaseEntity, IAclSupported;
    }
}