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
    public class CafeOccasionsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.CafeOccasion, App.BLL.DTO.CafeOccasion> _mapper;
        private readonly IMapper _autoMapper = null!;

        public CafeOccasionsController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.CafeOccasion, App.BLL.DTO.CafeOccasion>(autoMapper);
        }
        
        private Guid UserGuid
        {
            get
            {
                var userId = _userManager.GetUserId(User);
                return userId != null ? Guid.Parse(userId) : Guid.Empty;
            }
        }

        /// <summary>
        /// Returns all cafe occasions visible to current user
        /// </summary>
        /// <returns>list of cafe occasions</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.CafeOccasion>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.CafeOccasion>>> GetCafeOccasions()
        {
            var res = (await _bll.CafeOccasions.GetAllAsync(UserGuid
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Returns a single cafe occasion by id
        /// </summary>
        /// <param name="id">The ID of the cafe occasion to retrieve</param>
        /// <returns>A cafe occasion object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.CafeOccasion>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.CafeOccasion>> GetCafeOccasion(Guid id)
        {
            var cafeOccasion = _mapper.Map((await _bll.CafeOccasions.FirstOrDefaultAsync(id))!);
            if (cafeOccasion == null)
            {
                return NotFound();
            }
            return Ok(cafeOccasion);
        }

        /// <summary>
        /// Updates a specific cafe occasion
        /// </summary>
        /// <param name="id">The ID of the cafe occasion to update</param>
        /// <param name="cafeOccasion">The updated cafe occasion data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> PutCafeOccasion(Guid id, App.DTO.v1_0.CafeOccasion cafeOccasion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cafeOccasion.Id = id;
            _bll.CafeOccasions.Update(_mapper.Map(cafeOccasion));

            return NoContent();
            // if (id != cafeOccasion.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.CafeOccasions.Update(_mapper.Map(cafeOccasion)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.CafeOccasions.ExistsAsync(id))
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
        /// Adds a new cafe occasion
        /// </summary>
        /// <param name="cafeOccasion">Data transfer object representing the cafe occasion to add</param>
        /// <returns>The created cafe occasion</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.CafeOccasion>((int)HttpStatusCode.Created)]
        [Produces("application/json")]
        [Consumes("application/json")]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<ActionResult<App.DTO.v1_0.CafeOccasion>> PostCafeOccasion(
            App.DTO.v1_0.CafeOccasion cafeOccasion)
        {
            cafeOccasion.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCafeOc = _bll.CafeOccasions.Add(_mapper.Map(cafeOccasion)!);

            return CreatedAtAction("GetCafeOccasion",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newCafeOc.Id }, newCafeOc);

            // var addedCafeOccasion = await Task.Run(() => _bll.CafeOccasions.Add(_mapper.Map(cafeOccasion)));
            // var resultCafeOccasion = _mapper.Map(addedCafeOccasion);
            //
            // return CreatedAtAction("GetCafeOccasion", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultCafeOccasion.Id
            // }, resultCafeOccasion); }
        }

        /// <summary>
        /// Deletes a specific cafe occasion
        /// </summary>
        /// <param name="id">The ID of the cafe occasion to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteCafeOccasion(Guid id)
        {
            await _bll.CafeOccasions.RemoveAsync(id, UserGuid);
            return NoContent();
            // var cafeOccasionExists = await _bll.CafeOccasions.ExistsAsync(id);
            // if (!cafeOccasionExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.CafeOccasions.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            // return NoContent();
        }
    }
}
