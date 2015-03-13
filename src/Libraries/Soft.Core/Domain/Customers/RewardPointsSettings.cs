using Soft.Core.Configuration;
using Soft.Core.Domain.Orders;

namespace Soft.Core.Domain.Customers
{
    public class RewardPointsSettings : ISettings
    {
        /// <summary>
        /// Si el programa de puentos esta activo
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Valor del Reward Points con tipo de cambio
        /// </summary>
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// Minima cantidad de puntos para usar       
        /// </summary>
        public int MinimumRewardPointsToUse { get; set; }

        /// <summary>
        /// Numero de´puntos para registrarse
        /// </summary>
        public int PointsForRegistration { get; set; }

        /// <summary>
        /// Numero de puntos otorgados por compras 
        /// (Monto en monedas en el almacen principal)
        /// </summary>
        public decimal PointsForPurchasesAmount { get; set; }

        /// <summary>
        /// Numero de puntos otorgados por compras
        /// </summary>
        public int PointsForPurchasesPoints { get; set; }

        /// <summary>
        /// Puntos otorgados cuando la orden de compra esta
        /// </summary>
        public OrderStatus PointsForPurchasesAwarded { get; set; }

        /// <summary>
        /// Puntos otorgados cuando la orden de compra esta
        /// </summary>
        public OrderStatus PointsForPurchasesCanceled { get; set; }

        /// <summary>
        /// Si el mensaje deberia aparecer "You will earn"
        /// </summary>
        public bool DisplayHowMuchWillBeEarned { get; set; }
    }
}