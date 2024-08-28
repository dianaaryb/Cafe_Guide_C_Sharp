using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    // [Authorize]
    public class CafeTypesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public CafeTypesController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: CafeTypes
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)); //saan ainult selle kasutaja asjad
            var res = await _bll.CafeTypes.GetAllAsync(userId);
            return View(res);
        }

        // GET: CafeTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cafeType = await _bll.CafeTypes.FirstOrDefaultAsync(id.Value);
            if (cafeType == null)
            {
                return NotFound();
            }
        
            return View(cafeType);
        }
        
        // GET: CafeTypes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CafeId"] = new SelectList(await _bll.Cafes.GetAllAsync(), "Id", "Id");
            ViewData["TypeOfCafeId"] = new SelectList(await _bll.TypeOfCafes.GetAllAsync(), "Id", "Id");
            return View();
        }
        
        // POST: CafeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.BLL.DTO.CafeType cafeType)
        {
            if (ModelState.IsValid)
            {
                cafeType.Id = Guid.NewGuid();
                _bll.CafeTypes.Add(cafeType);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", cafeType.CafeId);
            // ViewData["TypeOfCafeId"] = new SelectList(_context.TypeOfCafes, "Id", "Id", cafeType.TypeOfCafeId);
            return View(cafeType);
        }
        
        // GET: CafeTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var cafeType = await _bll.CafeTypes.FirstOrDefaultAsync(id.Value);
            if (cafeType == null)
            {
                return NotFound();
            }
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", cafeType.CafeId);
            // ViewData["TypeOfCafeId"] = new SelectList(_context.TypeOfCafes, "Id", "Id", cafeType.TypeOfCafeId);
            return View(cafeType);
        }
        
        // POST: CafeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CafeId,TypeOfCafeId,Id")] App.BLL.DTO.CafeType cafeType)
        {
            if (id != cafeType.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.CafeTypes.Update(cafeType);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.CafeTypes.ExistsAsync(cafeType.Id))
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
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", cafeType.CafeId);
            // ViewData["TypeOfCafeId"] = new SelectList(_context.TypeOfCafes, "Id", "Id", cafeType.TypeOfCafeId);
            return View(cafeType);
        }
        
        // GET: CafeTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cafeType = await _bll.CafeTypes.FirstOrDefaultAsync(id.Value);
            if (cafeType == null)
            {
                return NotFound();
            }
        
            return View(cafeType);
        }
        
        // POST: CafeTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.CafeTypes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
