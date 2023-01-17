using System;
using System.Collections.Generic;
using System.Text;

namespace FabricaAutomotor.Microservicio.Ventas.Models
{
    /// <summary>
    /// Fee Data
    /// </summary>
    public class FeeData
    {
        /// <summary>
        /// Fee ID
        /// </summary>
        public decimal ID { get; set; }
        /// <summary>
        /// Item ID
        /// </summary>
        public decimal ItemID { get; set; }
        /// <summary>
        /// Fee percentage
        /// </summary>
        public int Percentage { get; set; }
    }
}