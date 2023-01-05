using System.Collections.Generic;
using FabricaAutomotor.Microservicio.Ventas.Models;
using FabricaAutomotor.Microservicio.Ventas.Models.Response;

namespace FabricaAutomotor.Microservicio.Ventas.Domain.Services
{
    public interface ISaleService
    {
        public void InsertSale(SaleData saleData);
        public decimal GetTotalSalesCount();
        public decimal GetTotalSalesCountFromStore(decimal storeID);
        public List<ItemSoldPercentageByStoreResponse> GetItemSalePercentageByStore();
    }
}
