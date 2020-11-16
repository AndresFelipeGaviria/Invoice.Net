using Factura.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Models
{
    public class Invoice 
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string NameClient { get; set; }
        public string NameShopkeeper { get; set; }
       
    }
    
       
    
}
