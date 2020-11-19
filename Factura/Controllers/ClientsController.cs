using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return await _facturaContexto.Clients.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var clientItem = await _facturaContexto.Clients.FindAsync(id);

            if (clientItem == null)
            {
                return NotFound();
            }
            return clientItem;
        }
        [HttpPost]
        public async Task<ActionResult<ClientDto>> PostClient(ClientDto item)
        {
            var cli = new Client();

            cli.Name = item.Name;
            cli.Telephone = item.Telephone;
            cli.Address = item.Address;

            _facturaContexto.Clients.Add(cli);
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
            var clientItem = await _facturaContexto.Clients.FindAsync(id);
            if (clientItem == null)
            {
                return NotFound();
            }
            _facturaContexto.Clients.Remove(clientItem);
            await _facturaContexto.SaveChangesAsync();

            return NoContent();
        }
       //[HttpPost]
       //public async Task<ActionResult> Upser
    }
}
