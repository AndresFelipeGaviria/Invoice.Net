using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Dto
{
    public class InvoiceFullDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public int DetailInvoiceId { get; set; }
        public string NameShopkeeper { get; set; }
        public List<DetailInvoiceDto> DetailInvoiceDto { get; set; }
        public ClientDto ClientDto { get; set; }


    }

    public class ResponseInvoice
    {
        public int InvoiceId { get; set; }
        public int IdClient { get; set; }
        public string NameClient { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public decimal PriceProduct { get; set; }

    }
    
}
