using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiparislerController : ControllerBase
    {
        private readonly UygulamaDbContext _context;

        public SiparislerController(UygulamaDbContext context)
        {
            _context = context;
        }

        // GET: api/Siparisler
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Siparis>>> GetSiparisler()
        {
            return await _context.Siparisler
                .Include(s => s.Kullanici)
                .Include(s => s.Kitap)
                .ToListAsync();
        }

        // GET: api/Siparisler/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Siparis>> GetSiparis(int id)
        {
            var siparis = await _context.Siparisler
                .Include(s => s.Kullanici)
                .Include(s => s.Kitap)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (siparis == null)
            {
                return NotFound();
            }

            return siparis;
        }

        // POST: api/Siparisler
        [HttpPost]
        public async Task<ActionResult<Siparis>> PostSiparis(Siparis siparis)
        {
            _context.Siparisler.Add(siparis);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSiparis), new { id = siparis.Id }, siparis);
        }

        // PUT: api/Siparisler/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiparis(int id, Siparis siparis)
        {
            if (id != siparis.Id)
            {
                return BadRequest();
            }

            _context.Entry(siparis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Siparisler.Any(e => e.Id == id))
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

        // DELETE: api/Siparisler/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiparis(int id)
        {
            var siparis = await _context.Siparisler.FindAsync(id);
            if (siparis == null)
            {
                return NotFound();
            }

            _context.Siparisler.Remove(siparis);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
