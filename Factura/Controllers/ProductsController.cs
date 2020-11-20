﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Factura.Data;
using Factura.Dto;
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
        private readonly IMapper _mapper;

        public ProductsController(FacturaContexto facturaContexto, IMapper mapper)
        {
            _mapper = mapper;
            _facturaContexto = facturaContexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var rs_product= await _facturaContexto.Products.ToListAsync();
            var map_prod = _mapper.Map<IEnumerable<ProductDto>>(rs_product);

            return Ok(map_prod); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var productItem = await _facturaContexto.Products.FindAsync(id);

            if(productItem == null)
            {
                return NotFound();
            }
            var map_produc = _mapper.Map<ProductDto>(productItem);
            return Ok(map_produc);
        }
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto item)
        {
            var map_produ = _mapper.Map<Product>(item);
            _facturaContexto.Products.Add(map_produ);
            await _facturaContexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, ProductDto item)
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
