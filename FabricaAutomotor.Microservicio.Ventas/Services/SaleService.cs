using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FabricaAutomotor.Microservicio.Ventas.Domain;
using FabricaAutomotor.Microservicio.Ventas.Domain.Repositories;
using FabricaAutomotor.Microservicio.Ventas.Domain.Services;
using FabricaAutomotor.Microservicio.Ventas.Models;
using FabricaAutomotor.Microservicio.Ventas.Models.Response;

namespace FabricaAutomotor.Microservicio.Ventas.API.Services
{
    /// <summary>
    /// Implements Sales Service class
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
    public class SaleService : ISaleService
    {
        #region Members
        private IRepository _repository;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public SaleService(IRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Insert new sale.
        /// </summary>
        /// <param name="saleData"></param>
        public void InsertSale(SaleData saleData)
        {
            _repository.InsertSale(saleData);
        }

        /// <summary>
        /// Shows the total amount of sales.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalSalesCount()
        {
            return _repository.GetTotalSalesCount();
        }

        /// <summary>
        /// Shows the total amount of sales by store.
        /// </summary>
        /// <param name="storeID"></param>
        /// <returns></returns>
        public decimal GetTotalSalesCountFromStore(decimal storeID)
        {
            return _repository.GetTotalSalesCountFromStore(storeID);
        }

        /// <summary>
        /// Shows the items sold impact of each store.
        /// </summary>
        /// <returns></returns>
        public List<ItemSoldPercentageByStoreResponse> GetItemSalePercentageByStore()
        {
            return _repository.GetItemSalePercentageByStore();
        }
        #endregion
    }
}
