using System.Collections.Generic;
using System.Linq;
using FabricaAutomotor.Microservicio.Ventas.Domain;

namespace FabricaAutomotor.Microservicio.Ventas.DataProvider
{
    public class MockedDataProvider : IDataProvider.IDataProvider
    {
        private List<StoreData> _storeDataList;
        private List<ItemData> _itemDataList;
        private List<FeeData> _feeDataList;

        private readonly static List<SaleData> _saleDataList = new List<SaleData>();
        public static List<SaleData> SaleDataList 
        {
            get
            {
                if(_saleDataList == null)
                {
                    return new List<SaleData>();
                }

                return SaleDataList;
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
        public List<int> GetItemFees(decimal ItemID)
        {
            return _feeDataList.Where(x => x.ItemID == ItemID).Select(x => x.Percentage).ToList();
        }

        public float GetItemPrice(decimal ItemID)
        {
            return _itemDataList.Where(x => x.ID == ItemID).Select(x => x.Price).FirstOrDefault();
        }

        public void InsertSale(SaleData saleData)
        {
            _saleDataList.Add(saleData);
        }
    }
}
