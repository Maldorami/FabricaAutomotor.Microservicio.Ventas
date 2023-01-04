using System;
using System.Collections.Generic;
using System.Text;
using FabricaAutomotor.Microservicio.Ventas.Domain;

namespace FabricaAutomotor.Microservicio.Ventas.IDataProvider
{
    public interface IDataProvider
    {
        public void InsertSale(SaleData saleData);
        public float GetItemPrice(decimal ItemID);
        public List<int> GetItemFees(decimal ItemID);
    }
}
