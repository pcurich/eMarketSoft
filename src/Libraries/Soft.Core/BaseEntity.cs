using System;

namespace Soft.Core
{
    /// <summary>
    /// Clase Base para entities
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Obtiene y establece el identificador para los entities
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Determina si el objeto <see cref="T:System.Object"/> especificado es igual al objeto <see cref="T:System.Object"/> actual.
        /// </summary>
        /// <returns>
        /// true si el objeto especificado es igual al objeto actual; de lo contrario, false.
        /// </returns>
        /// <param name="obj">Objeto que se va a comparar con el objeto actual.</param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        private static bool IsTransient(BaseEntity obj)
        {
            return obj != null && Equals(obj.Id, default(int));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(BaseEntity other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (IsTransient(this) || IsTransient(other) || !Equals(Id, other.Id))
                return false;

            var otherType = other.GetUnproxiedType();
            var thisType = GetUnproxiedType();

            return thisType.IsAssignableFrom(otherType) || otherType.IsAssignableFrom(thisType);
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(int)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !(x == y);
        }
    }
}