using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factura.Data;
using Factura.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Factura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly FacturaContexto _facturaContexto;

        public ProductsController(FacturaContexto facturaContexto)
        {
            _facturaContexto = facturaContexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _facturaContexto.Products.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var productItem = await _facturaContexto.Products.FindAsync(id);

            if(productItem == null)
            {
                return NotFound();
            }
            return productItem;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product item)
        {
            _facturaContexto.Products.Add(item);
            await _facturaContexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product item)
        {
            if(id != item.Id)
            {
                return BadRequest();
            }
            _facturaContexto.Entry(item).State = EntityState.Modified;
            await _facturaContexto.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var ProductItem = await _facturaContexto.Products.FindAsync(id);
            if(ProductItem == null)
            {
                return NotFound();
            }
            _facturaContexto.Products.Remove(ProductItem);
            await _facturaContexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
