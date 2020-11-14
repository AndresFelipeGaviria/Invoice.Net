using Factura.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Data
{
    public class FacturaContexto : DbContext

    {
        public FacturaContexto(DbContextOptions<FacturaContexto> options ):base(options)
        {
        }

        public DbSet<Client> ClientsItems { get; set; }
        public DbSet<Detail_Invoice> Detail_InvoiceItems { get; set; }
        public DbSet<Product> ProductItems { get; set; }
        public DbSet<Invoice> InvoiceItems { get; set; }
    }
}
