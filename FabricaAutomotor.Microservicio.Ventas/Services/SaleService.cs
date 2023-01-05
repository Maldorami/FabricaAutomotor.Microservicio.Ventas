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
    public class SaleService : ISaleService
    {
        private IRepository _repository;

        public SaleService(IRepository repository)
        {
            _repository = repository;
        }


        public void InsertSale(SaleData saleData)
        {
            _repository.InsertSale(saleData);
        }
        public decimal GetTotalSalesCount()
        {
            return _repository.GetTotalSalesCount();
        }
        public decimal GetTotalSalesCountFromStore(decimal storeID)
        {
            return _repository.GetTotalSalesCountFromStore(storeID);
        }

        public List<ItemSoldPercentageByStoreResponse> GetItemSalePercentageByStore()
        {
            return _repository.GetItemSalePercentageByStore();
        }
    }
}
