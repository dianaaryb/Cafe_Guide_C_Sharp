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
    public class CafeTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.CafeType, App.BLL.DTO.CafeType> _mapper;
        private readonly IMapper _autoMapper = null!;

        public CafeTypesController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.CafeType, App.BLL.DTO.CafeType>(autoMapper);

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
        /// Returns all cafe types visible to current user
        /// </summary>
        /// <returns>list of cafe types</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.CafeType>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.CafeType>>> GetCafeTypes()
        {
            var cafeTypes = (await _bll.CafeTypes.GetAllAsync(UserGuid))
                .Select(e => _mapper.Map(e));
            return Ok(cafeTypes);
        }

        /// <summary>
        /// Returns a single cafe type by id
        /// </summary>
        /// <param name="id">The ID of the cafe type to retrieve</param>
        /// <returns>A cafe type object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.CafeType>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.CafeType>> GetCafeType(Guid id)
        {
            var cafeType = await _bll.CafeTypes.FirstOrDefaultAsync(id);

            if (cafeType == null)
            {
                return NotFound();
            }

            var mappedType = _mapper.Map(cafeType);
            return Ok(mappedType);
        }

        /// <summary>
        /// Updates a specific cafe type
        /// </summary>
        /// <param name="id">The ID of the cafe type to update</param>
        /// <param name="cafeType">The updated cafe type data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> PutCafeType(Guid id, App.DTO.v1_0.CafeType cafeType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cafeType.Id = id;
            _bll.CafeTypes.Update(_mapper.Map(cafeType));

            return NoContent();
            // if (id != cafeType.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.CafeTypes.Update(_mapper.Map(cafeType)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.CafeTypes.ExistsAsync(id))
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
        /// Adds a new cafe type
        /// </summary>
        /// <param name="cafeType">Data transfer object representing the cafe type to add</param>
        /// <returns>The created cafe type</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.CafeType>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<ActionResult<App.DTO.v1_0.CafeType>> PostCafeType(App.DTO.v1_0.CafeType cafeType)
        {
            cafeType.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newCafeType = _bll.CafeTypes.Add(_mapper.Map(cafeType)!);
            
            return CreatedAtAction("GetCafetype",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newCafeType.Id }, newCafeType);

            // var addedCafeType = await Task.Run(() => _bll.CafeTypes.Add(_mapper.Map(cafeType)));
            // var resultCafeType = _mapper.Map(addedCafeType);
            //
            // return CreatedAtAction("GetCafeType", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultCafeType.Id
            // }, resultCafeType); 
        }

        /// <summary>
        /// Deletes a specific cafe type
        /// </summary>
        /// <param name="id">The ID of the cafe type to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteCafeType(Guid id)
        {
            await _bll.CafeTypes.RemoveAsync(id, UserGuid);
            return NoContent();
            // var cafeTypeExists = await _bll.CafeTypes.ExistsAsync(id);
            // if (!cafeTypeExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.CafeTypes.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            // return NoContent();
        }
    }
}
