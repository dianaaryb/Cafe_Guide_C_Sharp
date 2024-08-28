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
    public class CategoryOfCafesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.CategoryOfCafe, App.BLL.DTO.CategoryOfCafe> _mapper;
        private readonly IMapper _autoMapper = null!;

        public CategoryOfCafesController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.CategoryOfCafe, App.BLL.DTO.CategoryOfCafe>(autoMapper);

        }

        /// <summary>
        /// Returns all categories of cafe visible to current user
        /// </summary>
        /// <returns>list of categories of cafe</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.CategoryOfCafe>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.CategoryOfCafe>>> GetCategoryOfCafes()
        {
            var res = (await _bll.CategoryOfCafes.GetAllAsync(
                
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }
        
        /// <summary>
        /// Returns all categories for specific cafe
        /// </summary>
        /// <returns>list of categories of cafe</returns>
        [HttpGet("cat/{id}")]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.CategoryOfCafe>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.CategoryOfCafe>>> GetCategoriesForCafe(Guid id)
        {
            var res = (await _bll.CategoryOfCafes.GetAllForCafeAsync(id
                
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Returns a single category of cafe by id
        /// </summary>
        /// <param name="id">The ID of the category of cafe to retrieve</param>
        /// <returns>A category of cafe object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.CategoryOfCafe>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.CategoryOfCafe>> GetCategoryOfCafe(Guid id)
        {
            var categoryOfCafe = await _bll.CategoryOfCafes.FirstOrDefaultAsync(id);

            if (categoryOfCafe == null)
            {
                return NotFound();
            }
            var mappedCategoryOfCafe = _mapper.Map(categoryOfCafe);
            return Ok(mappedCategoryOfCafe);
        }

        /// <summary>
        /// Updates a specific category of cafe
        /// </summary>
        /// <param name="id">The ID of the category of cafe to update</param>
        /// <param name="categoryOfCafe">The updated category of cafe data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutCategoryOfCafe(Guid id, App.DTO.v1_0.CategoryOfCafe categoryOfCafe)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            categoryOfCafe.Id = id;
            _bll.CategoryOfCafes.Update(_mapper.Map(categoryOfCafe));

            return NoContent();
            // if (id != categoryOfCafe.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.CategoryOfCafes.Update(_mapper.Map(categoryOfCafe)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.CategoryOfCafes.ExistsAsync(id))
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
        /// Adds a new category of cafe
        /// </summary>
        /// <param name="categoryOfCafe">Data transfer object representing the category of cafe to add</param>
        /// <returns>The created category of cafe</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.CategoryOfCafe>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.CategoryOfCafe>> PostCategoryOfCafe(App.DTO.v1_0.CategoryOfCafe categoryOfCafe)
        {
            categoryOfCafe.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newCategoryOfCafe = _bll.CategoryOfCafes.Add(_mapper.Map(categoryOfCafe)!);
            
            return CreatedAtAction("GetCategoryOfCafe",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newCategoryOfCafe.Id }, newCategoryOfCafe);

            // var addedCategoryOfCafe = await Task.Run(() => _bll.CategoryOfCafes.Add(_mapper.Map(categoryOfCafe)));
            // var resultCategoryOfCafe = _mapper.Map(addedCategoryOfCafe);
            //
            // return CreatedAtAction("GetCategoryOfCafe", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultCategoryOfCafe.Id
            // }, resultCategoryOfCafe);
            //
        }

        /// <summary>
        /// Deletes a specific category of cafe
        /// </summary>
        /// <param name="id">The ID of the category of cafe to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteCategoryOfCafe(Guid id)
        {
            // var categoryOfCafeExists = await _bll.CategoryOfCafes.ExistsAsync(id);
            // if (!categoryOfCafeExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.CategoryOfCafes.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            await _bll.CategoryOfCafes.RemoveAsync(id);
            return NoContent();
        }
    }
}
