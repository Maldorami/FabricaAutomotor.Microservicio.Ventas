using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FabricaAutomotor.Microservicio.Ventas.Models;
using FabricaAutomotor.Microservicio.Ventas.Models.Response;

namespace FabricaAutomotor.Microservicio.Ventas.DataProvider
{
    public class MockedDataProvider : IDataProvider.IDataProvider
    {
        private List<StoreData> _storeDataList;
        private List<ItemData> _itemDataList;
        private List<FeeData> _feeDataList;

        private readonly static ConcurrentBag<SaleData> _saleDataList = new ConcurrentBag<SaleData>();
        public static ConcurrentBag<SaleData> SaleDataList 
        {
            get
            {
                if(_saleDataList == null)
                {
                    return new ConcurrentBag<SaleData>();
                }

                return _saleDataList;
            } 
        }

        public MockedDataProvider()
        {
            _storeDataList = new List<StoreData>()
            {
                new StoreData(){ ID = 1, Name = "Centro 1"},
                new StoreData(){ ID = 2, Name = "Centro 2"},
                new StoreData(){ ID = 3, Name = "Centro 3"},
                new StoreData(){ ID = 4, Name = "Centro 4"}
            };

            _itemDataList = new List<ItemData>()
            {
                new ItemData(){ID = 1, Description = "SEDAN", Price = 8000 },
                new ItemData(){ID = 2, Description = "SUV", Price = 9500 },
                new ItemData(){ID = 3, Description = "OFFROAD", Price = 12500 },
                new ItemData(){ID = 4, Description = "SPORT", Price = 18200 }
            };

            _feeDataList = new List<FeeData>()
            {
                new FeeData() { ID = 1, ItemID = 4, Percentage = 7}
            };
        }
        public void InsertSale(SaleData saleData)
        {
            SaleDataList.Add(saleData);
        }

        public decimal GetTotalSalesCount()
        {
            return SaleDataList.Count();
        }

        public decimal GetTotalSalesCountFromStore(decimal storeID)
        {
            return SaleDataList.Where(x => x.StoreID == storeID).Count();
        }

        public List<ItemSoldPercentageByStoreResponse> GetItemSalePercentageByStore()
        {
            var result = new List<ItemSoldPercentageByStoreResponse>();

            foreach (var storeSales in SaleDataList.OrderBy(x => x.StoreID).GroupBy(x => x.StoreID))
            {
                var store = new ItemSoldPercentageByStoreResponse();
                store.StoreName = _storeDataList.Where(x => x.ID == storeSales.Key).FirstOrDefault().Name;

                var itemListPercentage = new Dictionary<string, string>();

                foreach (var sales in storeSales.OrderBy(x => x.ItemID).GroupBy(x => x.ItemID))
                {
                    var itemPerc = sales.Count() * 100 / GetTotalSalesCount();
                    itemListPercentage.Add(_itemDataList.Where(x=> x.ID == sales.Key).FirstOrDefault().Description, 
                                                itemPerc.ToString("0.00") + '%');
                }
                store.SalesPercentageOverTotal = itemListPercentage;
                result.Add(store);
            }

            return result;
        }
        public List<int> GetItemFees(decimal ItemID)
        {
            return _feeDataList.Where(x => x.ItemID == ItemID).Select(x => x.Percentage).ToList();
        }

        public float GetItemPrice(decimal ItemID)
        {
            return _itemDataList.Where(x => x.ID == ItemID).Select(x => x.Price).FirstOrDefault();
        }
    }
}
