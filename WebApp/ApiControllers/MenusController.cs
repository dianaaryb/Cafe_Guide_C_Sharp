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
    public class MenusController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;
        private readonly PublicDTOBllMapper<App.DTO.v1_0.Menu, App.BLL.DTO.Menu> _mapper;
        private readonly IMapper _autoMapper = null!;

        public MenusController(IAppBLL bll, UserManager<AppUser> userManager, IMapper autoMapper)
        {
            _bll = bll;
            _userManager = userManager;
            _mapper = new PublicDTOBllMapper<App.DTO.v1_0.Menu, App.BLL.DTO.Menu>(autoMapper);

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
        /// Returns all menus visible to current user
        /// </summary>
        /// <returns>list of menus</returns>
        [HttpGet]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.Menu>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IEnumerable<App.DTO.v1_0.Menu>>> GetMenus()
        {
            var res = (await _bll.Menus.GetAllAsync(UserGuid)
            ).Select(e=> _mapper.Map(e));
            return Ok(res);
        }
        
        /// <summary>
        /// Returns all menus
        /// </summary>
        /// <returns>list of menus</returns>
        [HttpGet("all")]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.Menu>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Menu>>> GetAllMenus()
        {
            var res = (await _bll.Menus.GetAllAsync()
                ).Select(e=> _mapper.Map(e));
            return Ok(res);
        }
        
        /// <summary>
        /// Returns all menus for specific cafe
        /// </summary>
        /// <returns>list of menus</returns>
        [HttpGet("all/{cafeId}")]
        [ProducesResponseType<IEnumerable<App.BLL.DTO.Menu>>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<List<App.DTO.v1_0.Menu>>> GetAllMenusForCafe(Guid cafeId)
        {
            var res = (await _bll.Menus.GetAllAsync(cafeId)
                ).Select(e=> _mapper.Map(e)).ToList();
            return Ok(res);
        }

        /// <summary>
        /// Returns a single menu by id
        /// </summary>
        /// <param name="id">The ID of the menu to retrieve</param>
        /// <returns>A menu object</returns>
        [HttpGet("{id}")]
        [ProducesResponseType<App.BLL.DTO.Menu>((int) HttpStatusCode.OK)]
        [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Menu>> GetMenu(Guid id)
        {
            var menu = await _bll.Menus.FirstOrDefaultAsync(id);

            if (menu == null)
            {
                return NotFound();
            }
            var mappedMenu = _mapper.Map(menu);
            return Ok(mappedMenu);
        }
        
        // /// <summary>
        // /// Returns a single menu by id
        // /// </summary>
        // /// <param name="id">The ID of the cafe to retrieve</param>
        // /// <returns>A cafe object</returns>
        // [HttpGet("/{id}/withoutUser")]
        // [ProducesResponseType<App.BLL.DTO.Menu>((int) HttpStatusCode.OK)]
        // [ProducesResponseType((int) HttpStatusCode.NotFound)] 
        // [Produces( "application/json")]
        // [Consumes("application/json")]
        // public async Task<ActionResult<App.DTO.v1_0.Menu>> GetMenuWithoutUser(Guid id)
        // {
        //     var menu = _mapper.Map((await _bll.Menus.FirstOrDefaultAsync(id))!);
        //     if (menu == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return Ok(menu);
        // }

        /// <summary>
        /// Updates a specific menu
        /// </summary>
        /// <param name="id">The ID of the menu to update</param>
        /// <param name="menu">The updated menu data</param>
        /// <returns>A response indicating the result of the update operation</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [ProducesResponseType((int) HttpStatusCode.BadRequest)] 
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> PutMenu(Guid id, App.DTO.v1_0.Menu menu)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            menu.Id = id;
            _bll.Menus.Update(_mapper.Map(menu));

            return NoContent();
            // if (id != menu.Id)
            // {
            //     return BadRequest();
            // }
            // try
            // {
            //     var updated = await Task.Run(() => _bll.Menus.Update(_mapper.Map(menu)));
            //     if (updated == null)
            //     {
            //         return NotFound();
            //     }
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!await _bll.Menus.ExistsAsync(id))
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
        /// Adds a new menu
        /// </summary>
        /// <param name="menu">Data transfer object representing the menu to add</param>
        /// <returns>The created menu</returns>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType<App.BLL.DTO.Menu>((int) HttpStatusCode.Created)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<App.DTO.v1_0.Menu>> PostMenu(App.DTO.v1_0.Menu menu)
        {
            menu.Id = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            var newMenu = _bll.Menus.Add(_mapper.Map(menu)!);
            
            return CreatedAtAction("GetMenu",
                new { version = HttpContext.GetRequestedApiVersion()?.ToString(), id = newMenu.Id }, newMenu);


            // Console.WriteLine("in post metod");
            // var addedMenu = await Task.Run(() => _bll.Menus.Add(_mapper.Map(menu)));
            // var resultMenu = _mapper.Map(addedMenu);
            //
            // return CreatedAtAction("GetMenu", new
            // {
            //     version = HttpContext.GetRequestedApiVersion()?.ToString(),
            //     id = resultMenu.Id
            // }, resultMenu);
        }

        /// <summary>
        /// Deletes a specific menu
        /// </summary>
        /// <param name="id">The ID of the menu to delete</param>
        /// <returns>A response indicating the result of the delete operation</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int) HttpStatusCode.NotFound)]
        [ProducesResponseType((int) HttpStatusCode.NoContent)]
        [Produces( "application/json")]
        [Consumes("application/json")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            // var menuExists = await _bll.Menus.ExistsAsync(id);
            // if (!menuExists)
            // {
            //     return NotFound();
            // }
            // var rowsAffected = await _bll.Menus.RemoveAsync(id);
            // if (rowsAffected == 0)
            // {
            //     return NotFound();
            // }
            await _bll.Menus.RemoveAsync(id);
            return NoContent();
        }
    }
}
