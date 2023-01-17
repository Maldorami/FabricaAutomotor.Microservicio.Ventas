using System.Collections.Generic;
using FabricaAutomotor.Microservicio.Ventas.Models.Response;
using FabricaAutomotor.Microservicio.Ventas.Models;

namespace FabricaAutomotor.Microservicio.Ventas.IDataProvider
{
    public interface IDataProvider
    {
        public void InsertSale(SaleData saleData);
        public decimal GetTotalSalesCount();
        public decimal GetTotalSalesCountFromStore(decimal storeID);
        public List<ItemSoldPercentageByStoreResponse> GetItemSalePercentageByStore();
        public List<int> GetItemFees(decimal ItemID);
        public float GetItemPrice(decimal ItemID);
    }
}
