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

        //Authorised for standard users, If removed u dont need JWT key to use but Its more fun this way

        /// <summary>
        /// Get all Publishers
        /// </summary>
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Standard, Administrator")]

        public async Task<ActionResult<IEnumerable<PublisherDto>>> GetPublishers()
        {
            var publishers = _publisherRepository.GetPublishers();
            return Ok(publishers.Result.ToArray());
        }

        /// <summary>
        /// Gets one Specific Publisher by their Id      
        /// </summary>
        /// <param name="id">ID</param>
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

        /// <summary>
        /// Updates The Publisher by their ID
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="publisher"></param>
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<IActionResult> PutPublisher(int id, PublisherDto publisher)
        {
            var result = await _publisherRepository.UpdatePublisher(id, publisher);
            if (!result)
            {
                return NotFound();
            }

            return Ok("Publisher has been Updated");
        }

        /// <summary>
        /// Creates a Publisher
        /// 
        /// </summary>
        /// <param name="publisher"></param>
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

        /// <summary>
        /// Detetes a Publisher
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]

        public async Task<IActionResult> DeletePublisher(int id)
        {
            var result = await _publisherRepository.DeletePublisher(id);
            if (!result)
            {
                return NotFound();
            }

            return Ok("Publisher has been deleted");
        }
    }
}
