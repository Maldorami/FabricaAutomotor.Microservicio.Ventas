using System.Collections.Generic;
using FabricaAutomotor.Microservicio.Ventas.Domain.Repositories;
using FabricaAutomotor.Microservicio.Ventas.Models;
using FabricaAutomotor.Microservicio.Ventas.Models.Response;

namespace FabricaAutomotor.Microservicio.Ventas.Repository
{
    /// <summary>
    /// Implements Sales Repository class
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
    public class Repository : IRepository
    {
        #region Members
        private readonly IDataProvider.IDataProvider _dataProvider;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataProvider"></param>
        public Repository(IDataProvider.IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Insert new sale.
        /// </summary>
        /// <param name="saleData"></param>
        public void InsertSale(SaleData saleData)
        {
            saleData.ID = System.Guid.NewGuid();
            saleData.Price = CalculePrice(saleData.ItemID);

            _dataProvider.InsertSale(saleData);
        }

        /// <summary>
        /// Shows the total amount of sales.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalSalesCount()
        {
            return _dataProvider.GetTotalSalesCount();
        }

        /// <summary>
        /// Shows the total amount of sales by store.
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public decimal GetTotalSalesCountFromStore(decimal storeID)
        {
            return _dataProvider.GetTotalSalesCountFromStore(storeID);
        }

        /// <summary>
        /// Shows the items sold impact of each store.
        /// </summary>
        /// <returns></returns>
        public List<ItemSoldPercentageByStoreResponse> GetItemSalePercentageByStore()
        {
            return _dataProvider.GetItemSalePercentageByStore();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Calculate the price including the item fee.
        /// </summary>
        /// <param name="ItemID"></param>
        /// <returns></returns>
        private float CalculePrice(decimal ItemID)
        {
            float itemPrice = _dataProvider.GetItemPrice(ItemID);
            List<int> itemFees = _dataProvider.GetItemFees(ItemID);

            float result = ApplyFees(itemPrice, itemFees);

            return result;
        }
        /// <summary>
        /// Applies the item fee to the price.
        /// </summary>
        /// <param name="itemPrice"></param>
        /// <param name="itemFees"></param>
        /// <returns></returns>
        private float ApplyFees(float itemPrice, List<int> itemFees)
        {
            float fees = 0;

            foreach (var fee in itemFees)
            {
                fees += (itemPrice * fee) / 100;
            }

            return itemPrice + fees;
        }
        #endregion
    }
}
