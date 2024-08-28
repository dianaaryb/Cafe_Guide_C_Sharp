using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize]
    public class MenuItemsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public MenuItemsController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: MenuItems
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)); //saan ainult selle kasutaja asjad
            var res = await _bll.MenuItems.GetAllAsync(userId);
            return View(res);
        }

        // GET: MenuItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _bll.MenuItems.FirstOrDefaultAsync(id.Value);
            if (menuItem == null)
            {
                return NotFound();
            }
        
            return View(menuItem);
        }
        
        // GET: MenuItems/Create
        public async Task<IActionResult> Create()
        {
            ViewData["MenuId"] = new SelectList(await _bll.Menus.GetAllAsync(), "Id", "Id");
            ViewData["MenuItemCategoryId"] = new SelectList(await _bll.MenuItemCategories.GetAllAsync(), "Id", "Id");
            return View();
        }
        
        // POST: MenuItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.BLL.DTO.MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                menuItem.Id = Guid.NewGuid();
                _bll.MenuItems.Add(menuItem);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", menuItem.MenuId);
            // ViewData["MenuItemCategoryId"] = new SelectList(_context.MenuItemCategories, "Id", "Id", menuItem.MenuItemCategoryId);
            return View(menuItem);
        }
        
        // GET: MenuItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var menuItem = await _bll.MenuItems.FirstOrDefaultAsync(id.Value);
            if (menuItem == null)
            {
                return NotFound();
            }
            // ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", menuItem.MenuId);
            // ViewData["MenuItemCategoryId"] = new SelectList(_context.MenuItemCategories, "Id", "Id", menuItem.MenuItemCategoryId);
            return View(menuItem);
        }
        
        // POST: MenuItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MenuItemName,MenuItemDescription,MenuItemPrice,MenuItemCategoryId,MenuId,Id")] App.BLL.DTO.MenuItem menuItem)
        {
            if (id != menuItem.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.MenuItems.Update(menuItem);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.MenuItems.ExistsAsync(menuItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // ViewData["MenuId"] = new SelectList(_context.Menus, "Id", "Id", menuItem.MenuId);
            // ViewData["MenuItemCategoryId"] = new SelectList(_context.MenuItemCategories, "Id", "Id", menuItem.MenuItemCategoryId);
            return View(menuItem);
        }
        
        // GET: MenuItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItem = await _bll.MenuItems.FirstOrDefaultAsync(id.Value);
            if (menuItem == null)
            {
                return NotFound();
            }
        
            return View(menuItem);
        }
        
        // POST: MenuItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.MenuItems.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
