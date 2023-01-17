using System;
using System.Collections.Generic;
using System.Text;

namespace FabricaAutomotor.Microservicio.Ventas.Domain.Exceptions
{
    public class StoreNotExistsException : Exception
    {
        /// <summary>
        /// Custome Store Not Exists Exception.
        /// </summary>
        /// <param name="storeID"></param>
        public StoreNotExistsException(decimal storeID)
            : base(String.Format("La tienda con ID {0} no existe.", storeID)) { }
    }
}
