using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebApp.Helpers;

namespace WebApp.ApiControllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FavouritesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Favourite, App.BLL.DTO.Favourite> _mapper;
        private readonly IMapper _autoMapper = null!;

        public FavouritesController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Favourite, App.BLL.DTO.Favourite>(autoMapper);

        }

        /// <summary>
        /// Returns all favourites visible to current user
        /// </summary>
        /// <returns>list of favourites</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.Favourite>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Favourite>>> GetFavourites()
        {
            var res = (await _bll.Favourites.GetAllAsync(
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Returns a single favourite by id
        /// </summary>
        /// <param name="id">The ID of the favourite to retrieve</param>
        /// <returns>A favourite object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.Favourite>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Favourite>> GetFavourite(Guid id)
        {
            var favourite = _mapper.Map(await _bll.Favourites.FirstOrDefaultAsync(id));

            if (favourite == null)
            {
                return NotFound();
            }
            return Ok(favourite);
        }

        /// <summary>
        /// Updates a specific favourite
        /// </summary>
        /// <param name="id">The ID of the favourite to update</param>
        /// <param name="favourite">The updated favourite data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutFavourite(Guid id, App.DTO.v1_0.Favourite favourite)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            favourite.Id = id;
            _bll.Favourites.Update(_mapper.Map(favourite));

            return NoContent();
            // if (id != favourite.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.Favourites.Update(_mapper.Map(favourite)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.Favourites.ExistsAsync(id))
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
        /// Adds a new favourite
        /// </summary>
        /// <param name="favourite">Data transfer object representing the favourite to add</param>
        /// <returns>The created favourite</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.Favourite>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Favourite>> PostFavourite(App.DTO.v1_0.Favourite favourite)
        {
            favourite.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newFavourite = _bll.Favourites.Add(_mapper.Map(favourite)!);
            
            return CreatedAtAction("GetFavourite",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newFavourite.Id }, newFavourite);

        }

        /// <summary>
        /// Deletes a specific favourite
        /// </summary>
        /// <param name="id">The ID of the favourite to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteFavourite(Guid id)
        {
            // var favouriteExists = await _bll.Favourites.ExistsAsync(id);
            // if (!favouriteExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.Favourites.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            await _bll.Favourites.RemoveAsync(id);
            return NoContent();
        }
    }
}
