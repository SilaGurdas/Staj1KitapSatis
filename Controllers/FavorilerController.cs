using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavorilerController : ControllerBase
    {
        private readonly UygulamaDbContext _context;

        public FavorilerController(UygulamaDbContext context)
        {
            _context = context;
        }

   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Favori>>> GetFavoriler()
        {
            return await _context.Favoriler
                .Include(f => f.Kullanici)
                .Include(f => f.Kitap)
                .ToListAsync();
        }

      
        [HttpGet("{id}")]
        public async Task<ActionResult<Favori>> GetFavori(int id)
        {
            var favori = await _context.Favoriler
                .Include(f => f.Kullanici)
                .Include(f => f.Kitap)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (favori == null)
            {
                return NotFound();
            }

            return favori;
        }

        
        [HttpPost]
        public async Task<ActionResult<Favori>> PostFavori([FromBody] Favori favori)

        {
            _context.Favoriler.Add(favori);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFavori), new { id = favori.Id }, favori);
        }

  
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavori(int id, Favori favori)
        {
            if (id != favori.Id)
            {
                return BadRequest();
            }

            _context.Entry(favori).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Favoriler.Any(e => e.Id == id))
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
        public async Task<IActionResult> DeleteFavori(int id)
        {
            var favori = await _context.Favoriler.FindAsync(id);
            if (favori == null)
            {
                return NotFound();
            }

            _context.Favoriler.Remove(favori);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
