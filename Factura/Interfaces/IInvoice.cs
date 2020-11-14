using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Interfaces
{
    interface IInvoice
    {
        int AmountProduct();
        decimal Total();

    }
}
