using System;
using System.Collections.Generic;
using System.Text;

namespace FabricaAutomotor.Microservicio.Ventas.Models
{
    public class FeeData
    {
        public decimal ID { get; set; }
        public decimal ItemID { get; set; }
        public int Percentage { get; set; }
    }
}