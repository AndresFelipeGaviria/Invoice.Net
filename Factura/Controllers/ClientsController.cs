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
    public class ClientsController : ControllerBase
    {
        private readonly FacturaContexto _facturaContexto;

        public ClientsController(FacturaContexto facturaContexto)
        {
            _facturaContexto = facturaContexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetAllClients()
        {
            return await _facturaContexto.ClientsItems.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var clientItem = await _facturaContexto.ClientsItems.FindAsync(id);

            if (clientItem == null)
            {
                return NotFound();
            }
            return clientItem;
        }
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client item)
        {
            _facturaContexto.ClientsItems.Add(item);
            await _facturaContexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClient), new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutClient(int id, Client item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            _facturaContexto.Entry(item).State = EntityState.Modified;
            await _facturaContexto.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var clientItem = await _facturaContexto.ClientsItems.FindAsync(id);
            if (clientItem == null)
            {
                return NotFound();
            }
            _facturaContexto.ClientsItems.Remove(clientItem);
            await _facturaContexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
