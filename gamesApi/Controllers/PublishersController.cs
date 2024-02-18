using Microsoft.AspNetCore.Mvc;
using gamesApi.Models;
using gamesApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace gamesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        // GET: api/Publishers
        [HttpGet]
        //Authorised for standard users, If removed u dont need JWT key to use but Its more fun this way
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]

        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetPublishers()
        {
            var publishers = await _publisherRepository.GetPublishers();
            return Ok(publishers);
        }

        // GET: api/Publishers/5
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]

        public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
        {
            var publisher = await _publisherRepository.GetPublisherById(id);
            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
        }

        // PUT: api/Publishers/5
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<IActionResult> PutPublisher(int id, PublisherDto publisher)
        {
            var result = await _publisherRepository.UpdatePublisher(id, publisher);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Publishers
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<ActionResult<PublisherDto>> PostPublisher(PublisherDto publisher)
        {
            var result = await _publisherRepository.AddPublisher(publisher);
            if (!result)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetPublisher), new { id = publisher.PublisherId }, publisher);
        }

        // DELETE: api/Publishers/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<IActionResult> DeletePublisher(int id)
        {
            var result = await _publisherRepository.DeletePublisher(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
