using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using FabricaAutomotor.Microservicio.Ventas.Domain.Exceptions;
using FabricaAutomotor.Microservicio.Ventas.Models;
using FabricaAutomotor.Microservicio.Ventas.Models.Response;

namespace FabricaAutomotor.Microservicio.Ventas.DataProvider
{
    /// <summary>
    /// Implements Mocked Data Provider class
    /// </summary>
    /// <remarks>
    /// <list type="table">
    /// <listheader>
    ///     <term>Date</term>
    ///     <term>Who</term>
    ///     <description>Description</description>
    /// </listheader>
    ///  <item>
    ///        <term>17/01/2023</term>
    ///        <term>Ramiro Maldonado</term>
    ///        <description>Initial implementation</description>
    /// </item>
    /// </list>
    /// </remarks>
    public class MockedDataProvider : IDataProvider.IDataProvider
    {
        #region Properties
        /// <summary>
        /// Mocked list of available stores.
        /// </summary>
        private List<StoreData> _storeDataList;

        /// <summary>
        /// Mocked list of available items.
        /// </summary>
        private List<ItemData> _itemDataList;

        /// <summary>
        /// Mocked fee list.
        /// </summary>
        private List<FeeData> _feeDataList;

        /// <summary>
        /// Mocked sales list.
        /// </summary>
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
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
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
        #endregion

        #region Public Methods
        /// <summary>
        /// Insert new sale.
        /// </summary>
        /// <param name="saleData"></param>
        public void InsertSale(SaleData saleData)
        {
            if (!_storeDataList.Any(x => x.ID == saleData.StoreID)) throw new StoreNotExistsException(saleData.StoreID);
            if (!_itemDataList.Any(x => x.ID == saleData.ItemID)) throw new ItemNotExistsException(saleData.ItemID);
            SaleDataList.Add(saleData);
        }

        /// <summary>
        /// Shows the total amount of sales.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalSalesCount()
        {
            return SaleDataList.Count();
        }

        /// <summary>
        /// Shows the total amount of sales by store.
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public decimal GetTotalSalesCountFromStore(decimal storeID)
        {
            if (!_storeDataList.Any(x => x.ID == storeID)) throw new StoreNotExistsException(storeID);

            return SaleDataList.Where(x => x.StoreID == storeID).Count();
        }

        /// <summary>
        /// Shows the items sold impact of each store.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Get items fees.
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public List<int> GetItemFees(decimal ItemID)
        {
            return _feeDataList.Where(x => x.ItemID == ItemID).Select(x => x.Percentage).ToList();
        }

        /// <summary>
        /// Get item price with fees included.
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        public float GetItemPrice(decimal ItemID)
        {
            return _itemDataList.Where(x => x.ID == ItemID).Select(x => x.Price).FirstOrDefault();
        }

        #endregion
    }
}
