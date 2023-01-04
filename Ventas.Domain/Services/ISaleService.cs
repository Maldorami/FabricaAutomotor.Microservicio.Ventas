using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FabricaAutomotor.Microservicio.Ventas.Domain.Services
{
    public interface ISaleService
    {
        public void InsertSale(SaleData saleData); 
    }
}
