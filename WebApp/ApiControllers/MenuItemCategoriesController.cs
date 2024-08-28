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
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemCategoriesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.MenuItemCategory, App.BLL.DTO.MenuItemCategory> _mapper;
        private readonly IMapper _autoMapper = null!;

        public MenuItemCategoriesController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.MenuItemCategory, App.BLL.DTO.MenuItemCategory>(autoMapper);

        }

        /// <summary>
        /// Returns all menu item categories visible to current user
        /// </summary>
        /// <returns>list of menu item categories</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.MenuItemCategory>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.MenuItemCategory>>> GetMenuItemCategories()
        {
            var res = (await _bll.MenuItemCategories.GetAllAsync(
                
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Returns a single menu item category by id
        /// </summary>
        /// <param name="id">The ID of the menu item category to retrieve</param>
        /// <returns>A menu item category object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.MenuItemCategory>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.MenuItemCategory>> GetMenuItemCategory(Guid id)
        {
            var menuItemCategory = await _bll.MenuItemCategories.FirstOrDefaultAsync(id);

            if (menuItemCategory == null)
            {
                return NotFound();
            }
            var mappedMenuItemCategory = _mapper.Map(menuItemCategory);
            return Ok(mappedMenuItemCategory);
        }

        /// <summary>
        /// Updates a specific menu item category
        /// </summary>
        /// <param name="id">The ID of the menu item category to update</param>
        /// <param name="menuItemCategory">The updated menu item category data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutMenuItemCategory(Guid id, App.DTO.v1_0.MenuItemCategory menuItemCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            menuItemCategory.Id = id;
            _bll.MenuItemCategories.Update(_mapper.Map(menuItemCategory));

            return NoContent();
            // if (id != menuItemCategory.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.MenuItemCategories.Update(_mapper.Map(menuItemCategory)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.MenuItemCategories.ExistsAsync(id))
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
        /// Adds a new menu item category
        /// </summary>
        /// <param name="menuItmeCategory">Data transfer object representing the menu item category to add</param>
        /// <returns>The created menu item category</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.MenuItemCategory>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.MenuItemCategory>> PostMenuItemCategory(App.DTO.v1_0.MenuItemCategory menuItemCategory)
        {
            menuItemCategory.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newMenuItemCat = _bll.MenuItemCategories.Add(_mapper.Map(menuItemCategory)!);
            
            return CreatedAtAction("GetMenuItemCategory",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newMenuItemCat.Id }, newMenuItemCat);

            
        }

        /// <summary>
        /// Deletes a specific menu item category
        /// </summary>
        /// <param name="id">The ID of the menu item category to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteMenuItemCategory(Guid id)
        {
            await _bll.MenuItemCategories.RemoveAsync(id);
            // var menuItemCategoryExists = await _bll.MenuItemCategories.ExistsAsync(id);
            // if (!menuItemCategoryExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.MenuItemCategories.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            return NoContent();
        }
    }
}
