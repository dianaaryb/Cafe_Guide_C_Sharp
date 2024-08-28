using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize]
    public class MenuItemCategoriesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public MenuItemCategoriesController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: MenuItemCategories
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)); //saan ainult selle kasutaja asjad
            var res = await _bll.MenuItemCategories.GetAllAsync(userId);
            return View(res);
        }

        // GET: MenuItemCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemCategory = await _bll.MenuItemCategories.FirstOrDefaultAsync(id.Value);
            if (menuItemCategory == null)
            {
                return NotFound();
            }
        
            return View(menuItemCategory);
        }
        
        // GET: MenuItemCategories/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        
        // POST: MenuItemCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.BLL.DTO.MenuItemCategory menuItemCategory)
        {
            if (ModelState.IsValid)
            {
                menuItemCategory.Id = Guid.NewGuid();
                _bll.MenuItemCategories.Add(menuItemCategory);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(menuItemCategory);
        }
        
        // GET: MenuItemCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var menuItemCategory = await _bll.MenuItemCategories.FirstOrDefaultAsync(id.Value);
            if (menuItemCategory == null)
            {
                return NotFound();
            }
            return View(menuItemCategory);
        }
        
        // POST: MenuItemCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MenuItemCategoryName,Id")] App.BLL.DTO.MenuItemCategory menuItemCategory)
        {
            if (id != menuItemCategory.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.MenuItemCategories.Update(menuItemCategory);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.MenuItemCategories.ExistsAsync(menuItemCategory.Id))
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
            return View(menuItemCategory);
        }
        
        // GET: MenuItemCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemCategory = await _bll.MenuItemCategories.FirstOrDefaultAsync(id.Value);
            if (menuItemCategory == null)
            {
                return NotFound();
            }
        
            return View(menuItemCategory);
        }
        
        // POST: MenuItemCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.MenuItemCategories.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
