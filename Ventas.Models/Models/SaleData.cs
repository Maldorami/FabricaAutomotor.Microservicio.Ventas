using System;

namespace FabricaAutomotor.Microservicio.Ventas.Models
{
    /// <summary>
    /// A Sale.
    /// </summary>
    public class SaleData
    {
        /// <summary>
        /// Sale ID
        /// </summary>
        public Guid ID;

        /// <summary>
        /// Store ID
        /// </summary>
        public decimal StoreID;

        /// <summary>
        /// Item ID
        /// </summary>
        public decimal ItemID;

        /// <summary>
        /// Sale Price
        /// </summary>
        public float Price;
    }
}
