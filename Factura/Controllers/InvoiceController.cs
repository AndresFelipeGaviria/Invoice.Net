using Factura.Data;
using Factura.Dto;
using Factura.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Factura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly FacturaContexto _facturaContexto;

        public InvoiceController(FacturaContexto facturaContexto)
        {
            _facturaContexto = facturaContexto;
        }

        // GET: api/<InvoiceController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseInvoice>>> GetAllInvoices()
        {
            //
            var resultInvoices = await _facturaContexto.Invoices.Include("DetailsNavigations").Include("DetailsNavigations.Product").Include("DetailsNavigations").Include("ClientNavigation").ToListAsync();
            var response_invoice = new List<ResponseInvoice>();
            
            if (resultInvoices == null)
            {
                return BadRequest();
            }

            foreach (var invoice in resultInvoices)
            {


                var itemInvoce = new ResponseInvoice();
                itemInvoce.InvoiceId = invoice.Id;
                itemInvoce.IdClient = invoice.ClientNavigation.Id;
                itemInvoce.NameClient = invoice.ClientNavigation.Name;
                itemInvoce.Date = invoice.Date;

                itemInvoce.DetailInvoice = new List<DetailInvoiceDto>();
                    foreach (var detInvoice in invoice.DetailsNavigations)
                {
                    var itemDt = new DetailInvoiceDto();

                    itemDt.Product = new ProductDto {
                        Id = detInvoice.Product.Id,
                        Name = detInvoice.Product.Name,
                        Price = detInvoice.Product.Price
                    };

                    itemDt.Precio_Pro = detInvoice.Precio_Pro;
                    itemDt.Id = detInvoice.Id;
                    itemDt.ProductId = detInvoice.ProductId;
                    itemInvoce.DetailInvoice.Add(itemDt);
                    
                }
                    




                response_invoice.Add(itemInvoce);
            }
            return response_invoice;


        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<ActionResult<ResponseInvoice>> Post(InvoiceFullDto invoiceFullDto)
        {
            var result = new ResponseInvoice();
            try
            {
                using (var tran = _facturaContexto.Database.BeginTransaction())
                {
                    try
                    {

                        var Invoice = new Invoice();
                        Invoice.ClientId = invoiceFullDto.ClientId;
                        Invoice.NameShopkeeper = invoiceFullDto.NameShopkeeper;
                        Invoice.Date = DateTime.Now;

                        _facturaContexto.Invoices.Add(Invoice);
                        _facturaContexto.SaveChanges();

                        foreach (var item in invoiceFullDto.DetailInvoiceDto)
                        {
                            var listProduct = new DetailInvoice();
                            var prod = _facturaContexto.Products.Where(x => x.Id == item.ProductId).FirstOrDefault();
                            if (prod == null)
                                return new BadRequestResult();

                            listProduct.ProductId = item.ProductId;
                            listProduct.Precio_Pro = prod.Price;
                            listProduct.Cantidad = item.Cantidad;
                            listProduct.Total = prod.Price * item.Cantidad;
                            listProduct.InvoiceId = Invoice.Id;

                            _facturaContexto.DetailInvoices.Add(listProduct);
                            _facturaContexto.SaveChanges();
                        }


                        await tran.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await tran.RollbackAsync();
                        throw;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(result);
            //var resultProduct =  await _facturaContexto.Products.FindAsync(invoiceFullDto.DetailInvoiceDto.Product.Id);
            //if(resultProduct == null)
            //{
            //    return BadRequest();
            //}
            //var listProduct = new DetailInvoice();
            //listProduct.ProductId = resultProduct.Id;
            //listProduct.Precio_Pro = resultProduct.Price;
            //listProduct.Cantidad = invoiceFullDto.DetailInvoiceDto.Cantidad;
            //listProduct.Total = resultProduct.Price * listProduct.Cantidad;

            //_facturaContexto.DetailInvoices.Add(listProduct);
            //_facturaContexto.SaveChanges();


            //var findDetails = _facturaContexto.DetailInvoices.Where(x => )

            //var resultClient = await _facturaContexto.Clients.FindAsync(invoiceFullDto.ClientId);
            //if(resultClient == null)
            //{
            //    var client = new Client();
            //    client.Id = invoiceFullDto.ClientId;
            //    client.Name = invoiceFullDto.ClientDto.Name;
            //    client.Telephone = invoiceFullDto.ClientDto.Telephone;
            //    client.Address = invoiceFullDto.ClientDto.Address;

            //    _facturaContexto.Clients.Add(client);
            //    _facturaContexto.SaveChanges();
            //}
            //var Invoice = new Invoice();
            //Invoice.ClientId = resultClient.Id;
            //Invoice.NameShopkeeper = invoiceFullDto.NameShopkeeper;
            //Invoice.DetailInvoiceId = invoiceFullDto.DetailInvoiceId;

            //_facturaContexto.Invoices.Add(Invoice);
            //_facturaContexto.SaveChanges();



            //var findInvoice = await _facturaContexto.Invoices.Where(x => x.Id == Invoice.Id).FirstOrDefaultAsync();

            //var itemInvoce = new ResponseInvoice();
            //itemInvoce.InvoiceId = findInvoice.Id;
            //itemInvoce.IdClient = findInvoice.ClientNavigation.Id;
            //itemInvoce.NameClient = findInvoice.ClientNavigation.Name;
            //itemInvoce.ProductName = findInvoice.DetailsNavigations.Product.Name;
            //itemInvoce.PriceProduct = findInvoice.DetailsNavigations.Precio_Pro;

            //return itemInvoce;
        }

        // PUT api/<InvoiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InvoiceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
