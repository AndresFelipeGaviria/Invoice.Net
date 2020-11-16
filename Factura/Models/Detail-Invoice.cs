using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Models
{
    public class Detail_Invoice
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public decimal Precio_Pro { get; set; }
    }
}
