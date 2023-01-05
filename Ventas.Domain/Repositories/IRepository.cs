using System;
using System.Collections.Generic;
using FabricaAutomotor.Microservicio.Ventas.Models;
using System.Text;
using FabricaAutomotor.Microservicio.Ventas.Models.Response;

namespace FabricaAutomotor.Microservicio.Ventas.Domain.Repositories
{
    public interface IRepository
    {
        public void InsertSale(SaleData saleData);
        public decimal GetTotalSalesCount();
        public decimal GetTotalSalesCountFromStore(decimal storeID);
        public List<ItemSoldPercentageByStoreResponse> GetItemSalePercentageByStore();
    }
}
