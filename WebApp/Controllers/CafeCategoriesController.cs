using System.Security.Claims;
using App.BLL.DTO;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    // [Authorize]
    public class CafeCategoriesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public CafeCategoriesController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: CafeCategories
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!); //saan ainult selle kasutaja asjad
            var res = await _bll.CafeCategories.GetAllAsync(userId);
            return View(res);
        }
        
        // GET: CafeCategories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cafeCategory = await _bll.CafeCategories.FirstOrDefaultAsync(id.Value);
            if (cafeCategory == null)
            {
                return NotFound();
            }
        
            return View(cafeCategory);
        }
        
        // GET: CafeCategories/Create
        public async Task<IActionResult> Create()
        {
            
                // Pre-set the AppUserId to the currently logged-in user's ID
                // var appUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                // ViewData["AppUserId"] = appUserId;
            ViewData["CafeId"] = new SelectList(await _bll.Cafes.GetAllAsync(), "Id", "Id");
            ViewData["CategoryOfCafeId"] = new SelectList(await _bll.CategoryOfCafes.GetAllAsync(), "Id", "Id");
            return View();
        }
        
        // POST: CafeCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CafeCategory cafeCategory)
        {
            if (ModelState.IsValid)
            {
                cafeCategory.Id = Guid.NewGuid();
                _bll.CafeCategories.Add(cafeCategory);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", cafeCategory.CafeId);
            // ViewData["CategoryOfCafeId"] = new SelectList(_context.CategoryOfCafes, "Id", "Id", cafeCategory.CategoryOfCafeId);
            return View(cafeCategory);
        }
        
        // GET: CafeCategories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var cafeCategory = await _bll.CafeCategories.FirstOrDefaultAsync(id.Value);
            if (cafeCategory == null)
            {
                return NotFound();
            }
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", cafeCategory.CafeId);
            // ViewData["CategoryOfCafeId"] = new SelectList(_context.CategoryOfCafes, "Id", "Id", cafeCategory.CategoryOfCafeId);
            return View(cafeCategory);
        }
        
        // POST: CafeCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CafeId,CategoryOfCafeId,Id")] CafeCategory cafeCategory)
        {
            if (id != cafeCategory.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.CafeCategories.Update(cafeCategory);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.CafeCategories.ExistsAsync(cafeCategory.Id))
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
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", cafeCategory.CafeId);
            // ViewData["CategoryOfCafeId"] = new SelectList(_context.CategoryOfCafes, "Id", "Id", cafeCategory.CategoryOfCafeId);
            return View(cafeCategory);
        }
        
        // GET: CafeCategories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cafeCategory = await _bll.CafeCategories.FirstOrDefaultAsync(id.Value);
            if (cafeCategory == null)
            {
                return NotFound();
            }
        
            return View(cafeCategory);
        }
        
        // POST: CafeCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.CafeCategories.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
