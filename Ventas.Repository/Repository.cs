using System;
using System.Collections.Generic;
using FabricaAutomotor.Microservicio.Ventas.Domain.Repositories;
using FabricaAutomotor.Microservicio.Ventas.Models;
using FabricaAutomotor.Microservicio.Ventas.Models.Response;

namespace FabricaAutomotor.Microservicio.Ventas.Repository
{
    public class Repository : IRepository
    {
        private readonly IDataProvider.IDataProvider _dataProvider;

        public Repository(IDataProvider.IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public void InsertSale(SaleData saleData)
        {
            saleData.ID = System.Guid.NewGuid();
            saleData.Price = CalculePrice(saleData.ItemID);

            _dataProvider.InsertSale(saleData);
        }

        public decimal GetTotalSalesCount()
        {
            return _dataProvider.GetTotalSalesCount();
        }

        public decimal GetTotalSalesCountFromStore(decimal storeID)
        {
            return _dataProvider.GetTotalSalesCountFromStore(storeID);
        }

        public List<ItemSoldPercentageByStoreResponse> GetItemSalePercentageByStore()
        {
            return _dataProvider.GetItemSalePercentageByStore();
        }

        private float CalculePrice(decimal ItemID)
        {
            float itemPrice = _dataProvider.GetItemPrice(ItemID);
            List<int> itemFees = _dataProvider.GetItemFees(ItemID);

            float result = ApplyFees(itemPrice, itemFees);

            return result;
        }

        private float ApplyFees(float itemPrice, List<int> itemFees)
        {
            float fees = 0;

            foreach (var fee in itemFees)
            {
                fees += (itemPrice * fee) / 100;
            }

            return itemPrice + fees;
        }
    }
}
