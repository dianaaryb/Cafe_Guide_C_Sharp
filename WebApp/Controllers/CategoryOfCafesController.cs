using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    // [Authorize]
    public class CategoryOfCafesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public CategoryOfCafesController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: CategoryOfCafes
        public async Task<IActionResult> Index()
        {
            // var userId = Guid.Parse(_userManager.GetUserId(User)!); //saan ainult selle kasutaja asjad
            var res = await _bll.CategoryOfCafes.GetAllAsync();
            return View(res);        }

        // GET: CategoryOfCafes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryOfCafe = await _bll.CategoryOfCafes.FirstOrDefaultAsync(id.Value);
            if (categoryOfCafe == null)
            {
                return NotFound();
            }
        
            return View(categoryOfCafe);
        }
        
        // GET: CategoryOfCafes/Create
        public IActionResult Create()
        {
            return View();
        }
        
        // POST: CategoryOfCafes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.BLL.DTO.CategoryOfCafe categoryOfCafe)
        {
            categoryOfCafe.Id = Guid.NewGuid();
            // if (!ModelState.IsValid)
            // {
                // foreach (var entry in ModelState)
                // {
                //     foreach (var error in entry.Value.Errors)
                //     {
                //         Console.WriteLine($"Field: {entry.Key} - Error: {error.ErrorMessage}");
                //     }
                // }
            // }
            if (ModelState.IsValid)
            {
                _bll.CategoryOfCafes.Add(categoryOfCafe);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryOfCafe);
        }
        
        // GET: CategoryOfCafes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var categoryOfCafe = await _bll.CategoryOfCafes.FirstOrDefaultAsync(id.Value);
            if (categoryOfCafe == null)
            {
                return NotFound();
            }
            return View(categoryOfCafe);
        }
        
        // POST: CategoryOfCafes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryOfCafeName,CategoryOfCafeDescription,Id")] App.BLL.DTO.CategoryOfCafe categoryOfCafe)
        {
            if (id != categoryOfCafe.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.CategoryOfCafes.Update(categoryOfCafe);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.CategoryOfCafes.ExistsAsync(categoryOfCafe.Id))
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
            return View(categoryOfCafe);
        }
        
        // GET: CategoryOfCafes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryOfCafe = await _bll.CategoryOfCafes.FirstOrDefaultAsync(id.Value);
            if (categoryOfCafe == null)
            {
                return NotFound();
            }
        
            return View(categoryOfCafe);
        }
        
        // POST: CategoryOfCafes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.CategoryOfCafes.DeleteCategoryAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
