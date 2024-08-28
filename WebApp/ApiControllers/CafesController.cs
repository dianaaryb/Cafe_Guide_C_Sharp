using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
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
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CafesController : ControllerBase
    {
        private readonly AppDbContext _ctx;
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Cafe, App.BLL.DTO.Cafe> _mapper;
        private readonly IMapper _autoMapper = null!;

        public CafesController(AppDbContext ctx, IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _ctx = ctx;
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Cafe, App.BLL.DTO.Cafe>(autoMapper);
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
        /// Returns all cafes visible to current user
        /// </summary>
        /// <returns>list of Cafes</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.Cafe>>((int) HttpStatusCode.OK)]
        // [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Cafe>>> GetCafes()
        {
            var cafes = (await _bll.Cafes.GetAllAsync(UserGuid))
                .Select(e => _mapper.Map(e));
            return Ok(cafes);
            // var res = (await _bll.Cafes.GetAllAsync(
            //     Guid.Parse(_userManager.GetUserId(User)!)
            //     )).Select(e=> _mapper.Map(e)).ToList();
            // return Ok(res);
        }
        
        /// <summary>
        /// Returns all cafes
        /// </summary>
        /// <returns>list of Cafes</returns>
        [HttpGet("all")]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.Cafe>>((int) HttpStatusCode.OK)]
        // [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Cafe>>> GetAllCafes()
        {
            var cafes = (await _bll.Cafes.GetAllAsync())
                .Select(e => _mapper.Map(e));
            return Ok(cafes);
        }
 
        /// <summary>
        /// Returns a single cafe by id
        /// </summary>
        /// <param name="id">The ID of the cafe to retrieve</param>
        /// <returns>A cafe object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.Cafe>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Cafe>> GetCafe(Guid id)
        {
            var cafe = _mapper.Map((await _bll.Cafes.FirstOrDefaultAsync(id, UserGuid))!);
            if (cafe == null)
            {
                return NotFound();
            }
            return Ok(cafe);
        }
        
        /// <summary>
        /// Returns a single cafe by id without user
        /// </summary>
        /// <param name="id">The ID of the cafe to retrieve</param>
        /// <returns>A cafe object</returns>
        [HttpGet("{id}/withoutUser")]
        [ProducesResponseType<App.BLL.DTO.Cafe>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Cafe>> GetCafeWithoutUser(Guid id)
        {
            var cafe = _mapper.Map((await _bll.Cafes.FirstOrDefaultAsync(id))!);
            if (cafe == null)
            {
                return NotFound();
            }

            return Ok(cafe);
        }

        /// <summary>
        /// Updates a specific cafe
        /// </summary>
        /// <param name="id">The ID of the cafe to update</param>
        /// <param name="cafe">The updated cafe data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PutCafe(Guid id, App.DTO.v1_0.Cafe cafe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cafe.Id = id;
            _bll.Cafes.Update(_mapper.Map(cafe));

            return NoContent();
            // if (id != cafe.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.Cafes.Update(_mapper.Map(cafe)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.Cafes.ExistsAsync(id))
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
        /// Adds a new cafe
        /// </summary>
        /// <param name="cafe">Data transfer object representing the cafe to add</param>
        /// <returns>The created cafe</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.Cafe>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Cafe>> PostCafe(App.DTO.v1_0.Cafe cafe)
        {
            cafe.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newCafe = _bll.Cafes.Add(_mapper.Map(cafe)!);
            
            return CreatedAtAction("GetCafe",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newCafe.Id }, newCafe);
            
            
            // var addedCafe = await Task.Run(() => _bll.Cafes.Add(_mapper.Map(cafe)));
            // await _bll.SaveChangesAsync();
            // var resultCafe = _mapper.Map(addedCafe);
            // return CreatedAtAction("GetCafe", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultCafe.Id
            // }, resultCafe);
        }

        /// <summary>
        /// Deletes a specific cafe
        /// </summary>
        /// <param name="id">The ID of the cafe to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteCafe(Guid id)
        {
            await _bll.Cafes.RemoveAsync(id);
            return NoContent();
            // var cafeExists = await _bll.Cafes.ExistsAsync(id);
            // if (!cafeExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.Cafes.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            // return NoContent();
        }
    }
}
