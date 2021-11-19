using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTienda.Models.Entities
{
    public class factura
    {
        public int id_invoice{ get; set; }
        public int id_cliente { get; set; }
        public int codigo { get; set; }
        public List<detalles_factura> ListaDetalles { get; set; }

    }
}
