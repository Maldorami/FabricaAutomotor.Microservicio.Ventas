using System;
using FabricaAutomotor.Microservicio.Ventas.Domain;
using FabricaAutomotor.Microservicio.Ventas.Models;
using FabricaAutomotor.Microservicio.Ventas.Models.Request;

namespace FabricaAutomotor.Microservicio.Ventas.API.Factories
{
    public class SaleFactory
    {
        public static SaleData CreateSaleDataFrom(SaleRequest saleRequest)
        {
            return new SaleData()
            {
                StoreID = Convert.ToDecimal(saleRequest.StoreID),
                ItemID = Convert.ToDecimal(saleRequest.ItemID)
            };
        }
    }
}
