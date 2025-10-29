using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategorilerController : ControllerBase
    {
        private readonly UygulamaDbContext _context;

        public KategorilerController(UygulamaDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kategori>>> GetKategoriler()
        {
            return await _context.Kategoriler.Include(k => k.Kitaplar).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kategori>> GetKategori(int id)
        {
            var kategori = await _context.Kategoriler.Include(k => k.Kitaplar)
                                                     .FirstOrDefaultAsync(k => k.Id == id);

            if (kategori == null)
            {
                return NotFound();
            }

            return kategori;
        }

      
        [HttpPost]
        public async Task<ActionResult<Kategori>> PostKategori(Kategori kategori)
        {
            _context.Kategoriler.Add(kategori);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKategori), new { id = kategori.Id }, kategori);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKategori(int id, Kategori kategori)
        {
            if (id != kategori.Id)
            {
                return BadRequest();
            }

            _context.Entry(kategori).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Kategoriler.Any(e => e.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKategori(int id)
        {
            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }

            _context.Kategoriler.Remove(kategori);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
