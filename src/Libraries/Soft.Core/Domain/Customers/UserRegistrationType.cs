namespace Soft.Core.Domain.Customers
{
    /// <summary>
    /// Representa el tipo de registro de im cliente para el formato
    /// <c>Cuenta standar,</c>
    /// <c>Validado por Email,</c>
    /// <c>Aprobacion por el admin,</c>
    /// <c>Desabilitado,</c>
    /// </summary>
    public enum UserRegistrationType
    {
        /// <summary>
        /// Creacion de cuenta Standar
        /// </summary>
        Standard = 1,

        /// <summary>
        /// Validacion por email requeridad despues del registro
        /// </summary>
        EmailValidation = 2,

        /// <summary>
        /// Cliente que deberia ser aprobado por el administraodr
        /// </summary>
        AdminApproval = 3,

        /// <summary>
        /// Registro desabilitado
        /// </summary>
        Disabled = 4,
    }
}