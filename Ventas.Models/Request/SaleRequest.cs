namespace FabricaAutomotor.Microservicio.Ventas.Models.Request
{
    /// <summary>
    /// A sale.
    /// </summary>
    public class SaleRequest
    {
        /// <summary>
        /// The store ID.
        /// </summary>
        /// <example>1</example>
        public decimal StoreID { get; set; }

        /// <summary>
        /// The item ID.
        /// </summary>
        /// <example>4</example>
        public decimal ItemID { get; set; }
    }
}