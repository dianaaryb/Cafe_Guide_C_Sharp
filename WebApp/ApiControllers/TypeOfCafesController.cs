using System.Net;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
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
    public class TypeOfCafesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.TypeOfCafe, App.BLL.DTO.TypeOfCafe> _mapper;
        private readonly IMapper _autoMapper = null!;

        public TypeOfCafesController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.TypeOfCafe, App.BLL.DTO.TypeOfCafe>(autoMapper);

        }

        /// <summary>
        /// Returns all type of cafes visible to current user
        /// </summary>
        /// <returns>list of type of cafes</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.TypeOfCafe>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.TypeOfCafe>>> GetTypeOfCafes()
        {
            var res = (await _bll.TypeOfCafes.GetAllAsync(
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }
        
        /// <summary>
        /// Returns all types for specific cafe
        /// </summary>
        /// <returns>list of type of cafes</returns>
        [HttpGet("typ/{id}")]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.TypeOfCafe>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.TypeOfCafe>>> GetTypesForCafe(Guid id)
        {
            var res = (await _bll.TypeOfCafes.GetAllForCafeAsync(id
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }

        
        
        /// <summary>
        /// Returns a single type of cafe by id
        /// </summary>
        /// <param name="id">The ID of the type of cafe to retrieve</param>
        /// <returns>A cafe category object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.TypeOfCafe>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.TypeOfCafe>> GetTypeOfCafe(Guid id)
        {
            var typeOfCafe = await _bll.TypeOfCafes.FirstOrDefaultAsync(id);

            if (typeOfCafe == null)
            {
                return NotFound();
            }
            var mappedTypeOfCafe = _mapper.Map(typeOfCafe);
            return Ok(mappedTypeOfCafe);
        }

        /// <summary>
        /// Updates a specific type of cafe
        /// </summary>
        /// <param name="id">The ID of the type of cafe to update</param>
        /// <param name="typeOfCafe">The updated type of cafe data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutTypeOfCafe(Guid id, App.DTO.v1_0.TypeOfCafe typeOfCafe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            typeOfCafe.Id = id;
            _bll.TypeOfCafes.Update(_mapper.Map(typeOfCafe));

            return NoContent();
            // if (id != typeOfCafe.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.TypeOfCafes.Update(_mapper.Map(typeOfCafe)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.TypeOfCafes.ExistsAsync(id))
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
        /// Adds a new type of cafe
        /// </summary>
        /// <param name="typeOfCafe">Data transfer object representing the type of cafe to add</param>
        /// <returns>The created type of cafe</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.TypeOfCafe>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.TypeOfCafe>> PostTypeOfCafe(App.DTO.v1_0.TypeOfCafe typeOfCafe)
        {
            typeOfCafe.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newTypeOfCafe = _bll.TypeOfCafes.Add(_mapper.Map(typeOfCafe)!);
            
            return CreatedAtAction("GetTypeOfCafe",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newTypeOfCafe.Id }, newTypeOfCafe);

            // var addedTypeOfCafe = await Task.Run(() => _bll.TypeOfCafes.Add(_mapper.Map(typeOfCafe)));
            // var resultTypeOfCafe = _mapper.Map(addedTypeOfCafe);
            //
            // return CreatedAtAction("GetTypeOfCafe", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultTypeOfCafe.Id
            // }, resultTypeOfCafe);
            
        }

        /// <summary>
        /// Deletes a specific type of cafe
        /// </summary>
        /// <param name="id">The ID of the type of cafe to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteTypeOfCafe(Guid id)
        {
            await _bll.TypeOfCafes.RemoveAsync(id);
            // var typeOfCafeExists = await _bll.TypeOfCafes.ExistsAsync(id);
            // if (!typeOfCafeExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.TypeOfCafes.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            return NoContent();
        }
    }
}
