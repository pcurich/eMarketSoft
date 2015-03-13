using System;
using System.Collections.Generic;
using Soft.Core.Domain.Discounts;

namespace Soft.Services.Discounts
{
    public static class DiscountExtensions
    {
        /// <summary>
        /// Gets the discount amount for the specified value
        /// </summary>
        /// <param name="discount">Discount</param>
        /// <param name="amount">Amount</param>
        /// <returns>The discount amount</returns>
        public static decimal GetDiscountAmount(this Discount discount, decimal amount)
        {
            if (discount == null)
                throw new ArgumentNullException("discount");


            decimal result;
            if (discount.UsePercentage)
                result = (decimal)((((float)amount) * ((float)discount.DiscountPercentage)) / 100f);
            else
                result = discount.DiscountAmount;

            if (result < decimal.Zero)
                result = decimal.Zero;

            return result;
        }

        /// <summary>
        /// Obtiene el descuento preferente (Maximo valor de todos los decuentos)
        /// </summary>
        /// <param name="discounts">Lista de descuentos a revisar</param>
        /// <param name="amount">monto</param>
        /// <returns>Descuento proferente</returns>
        public static Discount GetPreferredDiscount(this IList<Discount> discounts,
            decimal amount)
        {
            Discount preferredDiscount = null;
            var maximumDiscountValue = decimal.Zero;
            foreach (var discount in discounts)
            {
                var currentDiscountValue = discount.GetDiscountAmount(amount);
                if (currentDiscountValue > maximumDiscountValue)
                {
                    maximumDiscountValue = currentDiscountValue;
                    preferredDiscount = discount;
                }
            }

            return preferredDiscount;
        }

        /// <summary>
        /// Check whether a list of discounts already contains a certain discount intance
        /// </summary>
        /// <param name="discounts">A list of discounts</param>
        /// <param name="discount">Discount to check</param>
        /// <returns>Result</returns>
        public static bool ContainsDiscount(this IList<Discount> discounts,
            Discount discount)
        {
            if (discounts == null)
                throw new ArgumentNullException("discounts");

            if (discount == null)
                throw new ArgumentNullException("discount");

            foreach (var dis1 in discounts)
                if (discount.Id == dis1.Id)
                    return true;

            return false;
        }
    }
}
