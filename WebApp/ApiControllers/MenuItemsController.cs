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
    public class MenuItemsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.MenuItem, App.BLL.DTO.MenuItem> _mapper;
        private readonly IMapper _autoMapper = null!;

        public MenuItemsController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.MenuItem, App.BLL.DTO.MenuItem>(autoMapper);

        }

        /// <summary>
        /// Returns all menu items visible to current user
        /// </summary>
        /// <returns>list of menu items</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.MenuItem>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.MenuItem>>> GetMenuItems()
        {
            var res = (await _bll.MenuItems.GetAllAsync(
               
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }
        
        /// <summary>
        /// Returns all menu items for specific menu
        /// </summary>
        /// <returns>list of review photos</returns>
        [HttpGet("all/{menuId}")]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.ReviewPhoto>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.ReviewPhoto>>> GetAllMenuItemsForMenu(Guid menuId)
        {
            var res = (await _bll.MenuItems.GetAllAsync(menuId
                
            )).Select(e=> _mapper.Map(e));
            return Ok(res);
        }

        /// <summary>
        /// Returns a single menu item by id
        /// </summary>
        /// <param name="id">The ID of the menu item to retrieve</param>
        /// <returns>A menu item object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.MenuItem>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.MenuItem>> GetMenuItem(Guid id)
        {
            var menuItem = await _bll.MenuItems.FirstOrDefaultAsync(id);

            if (menuItem == null)
            {
                return NotFound();
            }
            var mappedMenuItem = _mapper.Map(menuItem);
            return Ok(mappedMenuItem);
        }

        /// <summary>
        /// Updates a specific menu item
        /// </summary>
        /// <param name="id">The ID of the menu item to update</param>
        /// <param name="menuItem">The updated menu item data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutMenuItem(Guid id, App.DTO.v1_0.MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            menuItem.Id = id;
            _bll.MenuItems.Update(_mapper.Map(menuItem));

            return NoContent();
            // if (id != menuItem.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.MenuItems.Update(_mapper.Map(menuItem)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.MenuItems.ExistsAsync(id))
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
        /// Adds a new menu item
        /// </summary>
        /// <param name="menuItem">Data transfer object representing the menu item to add</param>
        /// <returns>The created menu item</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.MenuItem>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.MenuItem>> PostMenuItem(App.DTO.v1_0.MenuItem menuItem)
        {
            menuItem.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newMenuItem = _bll.MenuItems.Add(_mapper.Map(menuItem)!);
            
            return CreatedAtAction("GetMenuItem",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newMenuItem.Id }, newMenuItem);

            // var addedMenuItem = await Task.Run(() => _bll.MenuItems.Add(_mapper.Map(menuItem)));
            // var resultMenuItem = _mapper.Map(addedMenuItem);
            //
            // return CreatedAtAction("GetMenuItem", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultMenuItem.Id
            // }, resultMenuItem); 
        }

        /// <summary>
        /// Deletes a specific menu item
        /// </summary>
        /// <param name="id">The ID of the menu item to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteMenuItem(Guid id)
        {
            await _bll.MenuItems.RemoveAsync(id);
            // var menuItemExists = await _bll.MenuItems.ExistsAsync(id);
            // if (!menuItemExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.MenuItems.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            return NoContent();
        }
    }
}
