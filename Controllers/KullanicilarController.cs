using BookStoreAPI.Data;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullanicilarController : ControllerBase
    {
        private readonly UygulamaDbContext _context;

        public KullanicilarController(UygulamaDbContext context)
        {
            _context = context;
        }

        // GET: api/Kullanicilar
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Kullanici>>> GetKullanicilar()
        {
            return await _context.Kullanicilar.ToListAsync();
        }

        // GET: api/Kullanicilar/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Kullanici>> GetKullanici(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);

            if (kullanici == null)
                return NotFound();

            return kullanici;
        }

        // POST: api/Kullanicilar
        [HttpPost]
        public async Task<ActionResult<Kullanici>> PostKullanici(Kullanici kullanici)
        {
            _context.Kullanicilar.Add(kullanici);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetKullanici), new { id = kullanici.Id }, kullanici);
        }

        // PUT: api/Kullanicilar/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKullanici(int id, Kullanici kullanici)
        {
            if (id != kullanici.Id)
                return BadRequest();

            _context.Entry(kullanici).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Kullanicilar.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Kullanicilar/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKullanici(int id)
        {
            var kullanici = await _context.Kullanicilar.FindAsync(id);
            if (kullanici == null)
                return NotFound();

            _context.Kullanicilar.Remove(kullanici);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
