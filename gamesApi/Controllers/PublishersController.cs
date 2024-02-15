using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using gamesApi.Models;

namespace gamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly GamesContext _context;

        public PublishersController(GamesContext context)
        {
            _context = context;
        }

        // GET: api/Publishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Publisher>>> GetPublisher()
        {
          if (_context.Publisher == null)
          {
              return NotFound();
          }
            var publishers = await _context.Publisher
                .Select(p => new PublisherDto(p.PublisherId)
                {

                    PublisherName = p.PublisherName,

                    //CODE TO SHOW GAMES PER PUBLISHER WHEN USING GET
                    /*PublisherGames = p.PublisherGames.Select(g =>
                        new GameDto(g.GameId,g.PublisherId) 
                        {
                            GameName = g.GameName,
                            GameDescription = g.GameDescription,
                            ReleaseDate = g.ReleaseDate,
                            Genre = g.Genre
                        }).ToList()*/
                })
                .ToListAsync();

            return Ok(publishers);
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Publisher>> GetPublisher(int id)
        {
          if (_context.Publisher == null)
          {
              return NotFound();
          }
            var publisher = await _context.Publisher.FindAsync(id);

            if (publisher == null)
            {
                return NotFound();
            }

            return publisher;
        }

        // PUT: api/Publishers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
        {
            if (id != publisher.PublisherId)
            {
                return BadRequest();
            }

            _context.Entry(publisher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        // POST: api/Publishers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher(Publisher publisher)
        {
          if (_context.Publisher == null)
          {
              return Problem("Entity set 'GamesContext.Publisher'  is null.");
          }
            _context.Publisher.Add(publisher);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPublisher), new { id = publisher.PublisherId }, publisher); 
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            if (_context.Publisher == null)
            {
                return NotFound();
            }
            var publisher = await _context.Publisher.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _context.Publisher.Remove(publisher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PublisherExists(int id)
        {
            return (_context.Publisher?.Any(e => e.PublisherId == id)).GetValueOrDefault();
        }
    }
}
