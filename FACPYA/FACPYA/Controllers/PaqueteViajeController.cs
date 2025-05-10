using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FACPYA.Models;

namespace FACPYA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaqueteViajeController : ControllerBase
    {
        private readonly DbfacpyaContext _context;

        public PaqueteViajeController(DbfacpyaContext context)
        {
            _context = context;
        }

        // GET: api/PaqueteViaje
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaqueteViaje>>> GetPaqueteViajes()
        {
            return await _context.PaqueteViajes.ToListAsync();
        }

        // GET: api/PaqueteViaje/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaqueteViaje>> GetPaqueteViaje(int id)
        {
            var paqueteViaje = await _context.PaqueteViajes.FindAsync(id);

            if (paqueteViaje == null)
            {
                return NotFound();
            }

            return paqueteViaje;
        }

        // PUT: api/PaqueteViaje/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaqueteViaje(int id, PaqueteViaje paqueteViaje)
        {
            if (id != paqueteViaje.IdPaquete)
            {
                return BadRequest();
            }

            _context.Entry(paqueteViaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaqueteViajeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PaqueteViaje
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaqueteViaje>> PostPaqueteViaje(PaqueteViaje paqueteViaje)
        {
            _context.PaqueteViajes.Add(paqueteViaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaqueteViaje", new { id = paqueteViaje.IdPaquete }, paqueteViaje);
        }

        // DELETE: api/PaqueteViaje/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaqueteViaje(int id)
        {
            var paqueteViaje = await _context.PaqueteViajes.FindAsync(id);
            if (paqueteViaje == null)
            {
                return NotFound();
            }

            _context.PaqueteViajes.Remove(paqueteViaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaqueteViajeExists(int id)
        {
            return _context.PaqueteViajes.Any(e => e.IdPaquete == id);
        }
    }
}
