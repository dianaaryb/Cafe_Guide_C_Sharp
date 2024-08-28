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
    public class MenusController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public MenusController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Menus
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)); //saan ainult selle kasutaja asjad
            var res = await _bll.Menus.GetAllAsync(userId);
            return View(res);
        }
        
        // GET: Menus/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _bll.Menus.FirstOrDefaultAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
        
            return View(menu);
        }
        
        // GET: Menus/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CafeId"] = new SelectList(await _bll.Cafes.GetAllAsync(), "Id", "CafeName");
            return View();
        }
        
        // POST: Menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.BLL.DTO.Menu menu)
        {
            Console.Write("in create method");
            
            if (ModelState.IsValid)
            {
                menu.Id = Guid.NewGuid();
                _bll.Menus.Add(menu);
                await _bll.SaveChangesAsync();
                Console.WriteLine("after saving ");
                return RedirectToAction(nameof(Index));
            }
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", menu.CafeId);
            return View(menu);
        }
        
        // GET: Menus/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var menu = await _bll.Menus.FirstOrDefaultAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", menu.CafeId);
            return View(menu);
        }
        
        // POST: Menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CafeId,Id")] App.BLL.DTO.Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Menus.Update(menu);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.Menus.ExistsAsync(menu.Id))
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
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", menu.CafeId);
            return View(menu);
        }
        
        // GET: Menus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _bll.Menus.FirstOrDefaultAsync(id.Value);
            if (menu == null)
            {
                return NotFound();
            }
        
            return View(menu);
        }
        
        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Menus.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}