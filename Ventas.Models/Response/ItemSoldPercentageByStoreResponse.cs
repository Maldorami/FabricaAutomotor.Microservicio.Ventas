using System.Collections.Generic;

namespace FabricaAutomotor.Microservicio.Ventas.Models.Response
{
    /// <summary>
    /// An Item Sale Percentage By Store.
    /// </summary>
    public class ItemSoldPercentageByStoreResponse
    {
        /// <summary>
        /// Store Data.
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// Item sold percentage list.
        /// </summary>
        public Dictionary<string, string> SalesPercentageOverTotal { get; set; }
    }
}