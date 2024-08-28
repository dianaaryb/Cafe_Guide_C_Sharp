using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class CitiesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.City, App.BLL.DTO.City> _mapper;
        private readonly IMapper _autoMapper = null!;

        public CitiesController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.City, App.BLL.DTO.City>(autoMapper);

        }

        /// <summary>
        /// Returns all cities visible to current user
        /// </summary>
        /// <returns>list of cities</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.City>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.City>>> GetCities()
        {
            var res = (await _bll.Cities.GetAllAsync(
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Returns a single city by id
        /// </summary>
        /// <param name="id">The ID of the city to retrieve</param>
        /// <returns>A city object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.City>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.City>> GetCity(Guid id)
        {
            var city = await _bll.Cities.FirstOrDefaultAsync(id);

            if (city == null)
            {
                return NotFound();
            }
            var mappedCity = _mapper.Map(city);
            return Ok(mappedCity);
        }

        /// <summary>
        /// Updates a specific city
        /// </summary>
        /// <param name="id">The ID of the city to update</param>
        /// <param name="city">The updated city data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutCity(Guid id, App.DTO.v1_0.City city)
        {
            if (id != city.Id)
            {
                return BadRequest();
            }
            try
            {
                var updated = await Task.Run(() => _bll.Cities.Update(_mapper.Map(city)));
                if (updated == null)
                {
                    return NotFound();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.Cities.ExistsAsync(id))
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

        /// <summary>
        /// Adds a new city
        /// </summary>
        /// <param name="city">Data transfer object representing the city to add</param>
        /// <returns>The created city</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.City>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.City>> PostCity(App.DTO.v1_0.City city)
        {
            var addedCity = await Task.Run(() => _bll.Cities.Add(_mapper.Map(city)));
            var resultCity = _mapper.Map(addedCity);

            return CreatedAtAction("GetCity", new
            {
                version = HttpContext.GetRequestedApiVersion()?.ToString(),
                id = resultCity.Id
            }, resultCity);  }

        /// <summary>
        /// Deletes a specific city
        /// </summary>
        /// <param name="id">The ID of the city to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var cityExists = await _bll.Cities.ExistsAsync(id);
            if (!cityExists)
            {
                return NotFound();
            }
            var rowsAffected = await _bll.Cities.RemoveAsync(id);
            if (rowsAffected == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
