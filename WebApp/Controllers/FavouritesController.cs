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
    public class FavouritesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public FavouritesController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Favourites
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)); //saan ainult selle kasutaja asjad
            var res = await _bll.Favourites.GetAllAsync(userId);
            return View(res);
        }
        
        // GET: Favourites/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _bll.Favourites.FirstOrDefaultAsync(id.Value);
            if (favourite == null)
            {
                return NotFound();
            }
        
            return View(favourite);
        }
        
        // GET: Favourites/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AppUserId"] = new SelectList(await _bll.AppUsers.GetAllAsync(), "Id", "FirstName");
            ViewData["CafeId"] = new SelectList(await _bll.Cafes.GetAllAsync(), "Id", "Id");
            return View();
        }
        
        // POST: Favourites/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.BLL.DTO.Favourite favourite)
        {
            if (ModelState.IsValid)
            {
                favourite.Id = Guid.NewGuid();
                _bll.Favourites.Add(favourite);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", favourite.AppUserId);
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", favourite.CafeId);
            return View(favourite);
        }
        
        // GET: Favourites/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var favourite = await _bll.Favourites.FirstOrDefaultAsync(id.Value);
            if (favourite == null)
            {
                return NotFound();
            }
            // ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", favourite.AppUserId);
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", favourite.CafeId);
            return View(favourite);
        }
        
        // POST: Favourites/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,CafeId,Id")] App.BLL.DTO.Favourite favourite)
        {
            if (id != favourite.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Favourites.Update(favourite);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.Favourites.ExistsAsync(favourite.Id))
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
            // ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", favourite.AppUserId);
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", favourite.CafeId);
            return View(favourite);
        }
        
        // GET: Favourites/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var favourite = await _bll.Favourites.FirstOrDefaultAsync(id.Value);
            if (favourite == null)
            {
                return NotFound();
            }
        
            return View(favourite);
        }
        
        // POST: Favourites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Favourites.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
