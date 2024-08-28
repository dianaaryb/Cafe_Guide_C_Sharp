using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OccasionOfCafesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.OccasionOfCafe, App.BLL.DTO.OccasionOfCafe> _mapper;
        private readonly IMapper _autoMapper = null!;

        public OccasionOfCafesController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.OccasionOfCafe, App.BLL.DTO.OccasionOfCafe>(autoMapper);

        }

        /// <summary>
        /// Returns all occasion of cafes visible to current user
        /// </summary>
        /// <returns>list of occasion of cafes</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.CafeCategory>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.OccasionOfCafe>>> GetOccasionOfCafes()
        {
            var res = (await _bll.OccasionOfCafes.GetAllAsync(
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }
        
        /// <summary>
        /// Returns all occasion for specific cafe
        /// </summary>
        /// <returns>list of occasion of cafes</returns>
        [HttpGet("oc/{id}")]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.OccasionOfCafe>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.OccasionOfCafe>>> GetOccasionsForCafe(Guid id)
        {
            var res = (await _bll.OccasionOfCafes.GetAllForCafeAsync(id
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Returns a single occasion of cafes by id
        /// </summary>
        /// <param name="id">The ID of the occasion of cafes to retrieve</param>
        /// <returns>A occasion of cafes object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.OccasionOfCafe>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.OccasionOfCafe>> GetOccasionOfCafe(Guid id)
        {
            var occasionOfCafe = await _bll.OccasionOfCafes.FirstOrDefaultAsync(id);

            if (occasionOfCafe == null)
            {
                return NotFound();
            }
            var mappedOccasionOfCafe = _mapper.Map(occasionOfCafe);
            return Ok(mappedOccasionOfCafe);
        }

        /// <summary>
        /// Updates a specific occasion of cafe
        /// </summary>
        /// <param name="id">The ID of the occasion of cafe to update</param>
        /// <param name="occasionOfCafe">The updated occasion of cafe data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutOccasionOfCafe(Guid id, App.DTO.v1_0.OccasionOfCafe occasionOfCafe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            occasionOfCafe.Id = id;
            _bll.OccasionOfCafes.Update(_mapper.Map(occasionOfCafe));

            return NoContent();
            // if (id != occasionOfCafe.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.OccasionOfCafes.Update(_mapper.Map(occasionOfCafe)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.OccasionOfCafes.ExistsAsync(id))
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }
            // return NoContent();
        }

        /// <summary>
        /// Adds a new occasion of cafe
        /// </summary>
        /// <param name="occasionOfCafe">Data transfer object representing the occasion of cafe to add</param>
        /// <returns>The created occasion of cafe</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.OccasionOfCafe>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.OccasionOfCafe>> PostOccasionOfCafe(App.DTO.v1_0.OccasionOfCafe occasionOfCafe)
        {
            occasionOfCafe.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newOccasionOfCafe = _bll.OccasionOfCafes.Add(_mapper.Map(occasionOfCafe)!);
            
            return CreatedAtAction("GetOccasionOfCafe",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newOccasionOfCafe.Id }, newOccasionOfCafe);

            // var addedOccasionOfCafe = await Task.Run(() => _bll.OccasionOfCafes.Add(_mapper.Map(occasionOfCafe)));
            // var resultOccasionOfCafe = _mapper.Map(addedOccasionOfCafe);
            //
            // return CreatedAtAction("GetOccasionOfCafe", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultOccasionOfCafe.Id
            // }, resultOccasionOfCafe);
            //
        }

        /// <summary>
        /// Deletes a specific occasion of cafe
        /// </summary>
        /// <param name="id">The ID of the occasion of cafe to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteOccasionOfCafe(Guid id)
        {
            await _bll.OccasionOfCafes.RemoveAsync(id);
            // var occasionOfCafeExists = await _bll.OccasionOfCafes.ExistsAsync(id);
            // if (!occasionOfCafeExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.OccasionOfCafes.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            return NoContent();
        }
    }
}
