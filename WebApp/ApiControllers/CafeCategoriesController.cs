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
    public class CafeCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.CafeCategory, App.BLL.DTO.CafeCategory> _mapper;
        private readonly IMapper _autoMapper = null!;

        public CafeCategoriesController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.CafeCategory, App.BLL.DTO.CafeCategory>(autoMapper);
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
        /// Returns all cafe categories visible to current user
        /// </summary>
        /// <returns>list of cafe categories</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.CafeCategory>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.CafeCategory>>> GetCafeCategories()
        {
            var res = (await _bll.CafeCategories.GetAllAsync(UserGuid)
            ).Select(e=> _mapper.Map(e));
            return Ok(res);
        }
        

        /// <summary>
        /// Returns a single cafe category by id
        /// </summary>
        /// <param name="id">The ID of the cafe category to retrieve</param>
        /// <returns>A cafe category object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.CafeCategory>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.CafeCategory>> GetCafeCategory(Guid id)
        {
            var cafeCategory = await _bll.CafeCategories.FirstOrDefaultAsync(id);

            if (cafeCategory == null)
            {
                return NotFound();
            }
            var mappedCafeCategory = _mapper.Map(cafeCategory);
            return Ok(mappedCafeCategory);
        }

        /// <summary>
        /// Updates a specific cafe category
        /// </summary>
        /// <param name="id">The ID of the cafe category to update</param>
        /// <param name="cafeCategory">The updated cafe category data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<IActionResult> PutCafeCategory(Guid id, App.DTO.v1_0.CafeCategory cafeCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            cafeCategory.Id = id;
            _bll.CafeCategories.Update(_mapper.Map(cafeCategory));

            return NoContent();
            // if (id != cafeCategory.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.CafeCategories.Update(_mapper.Map(cafeCategory)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.CafeCategories.ExistsAsync(id))
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
        /// Adds a new cafe category
        /// </summary>
        /// <param name="cafeCategory">Data transfer object representing the cafe category to add</param>
        /// <returns>The created cafe category</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.CafeCategory>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public async Task<ActionResult<App.DTO.v1_0.CafeCategory>> PostCafeCategory(App.DTO.v1_0.CafeCategory cafeCategory)
        {
            cafeCategory.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newCafeCategory = _bll.CafeCategories.Add(_mapper.Map(cafeCategory)!);
            
            return CreatedAtAction("GetCafeCategory",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newCafeCategory.Id }, newCafeCategory);

            // var addedCafeCategory = await Task.Run(() => _bll.CafeCategories.Add(_mapper.Map(cafeCategory)));
            // var resultCafeCategory = _mapper.Map(addedCafeCategory);
            //
            // return CreatedAtAction("GetCafeCategory", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultCafeCategory.Id
            // }, resultCafeCategory);
        }

        /// <summary>
        /// Deletes a specific cafe category
        /// </summary>
        /// <param name="id">The ID of the cafe category to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteCafeCategory(Guid id)
        {
            await _bll.CafeCategories.RemoveAsync(id, UserGuid);
            return NoContent();
            // var cafeCategoryExists = await _bll.CafeCategories.ExistsAsync(id);
            // if (!cafeCategoryExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.CafeCategories.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            // return NoContent();
        }
    }
}
