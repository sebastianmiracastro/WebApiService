using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTienda.Models.Entities
{
    public class detalles_factura
    {
        public int id_detalles { get; set; }
        public int id_invoice { get; set; }
        public string descripcion { get; set; }
        public string valor { get; set; }

    }
}
