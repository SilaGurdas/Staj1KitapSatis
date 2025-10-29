using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KitaplarController : ControllerBase
    {
        private readonly UygulamaDbContext _context;

        public KitaplarController(UygulamaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kitap>>> GetKitaplar()
        {
            return await _context.Kitaplar.Include(k => k.Kategori).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Kitap>> GetKitap(int id)
        {
            var kitap = await _context.Kitaplar.Include(k => k.Kategori).FirstOrDefaultAsync(k => k.Id == id);

            if (kitap == null)
            {
                return NotFound();
            }

            return kitap;
        }



        [HttpPost]
        public async Task<ActionResult<Kitap>> PostKitap(Kitap kitap)
        {
            _context.Kitaplar.Add(kitap);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetKitap", new { id = kitap.Id }, kitap);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutKitap(int id, Kitap kitap)
        {
            if (id != kitap.Id)
            {
                return BadRequest();
            }

            _context.Entry(kitap).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Kitaplar.Any(e => e.Id == id))
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
        public async Task<IActionResult> DeleteKitap(int id)
        {
            var kitap = await _context.Kitaplar.FindAsync(id);
            if (kitap == null)
            {
                return NotFound();
            }

            _context.Kitaplar.Remove(kitap);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
