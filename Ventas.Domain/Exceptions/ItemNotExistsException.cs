using System;
using System.Collections.Generic;
using System.Text;

namespace FabricaAutomotor.Microservicio.Ventas.Domain.Exceptions
{
    public class ItemNotExistsException : Exception
    {
        /// <summary>
        /// Custome Item Not Exists Exception.
        /// </summary>
        /// <param name="itemID"></param>
        public ItemNotExistsException(decimal itemID)
            : base(String.Format("El ítem con ID {0} no existe.", itemID)) { }
    }
}
