using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CafeOccasion = App.BLL.DTO.CafeOccasion;

namespace WebApp.Controllers
{
    // [Authorize]
    public class CafeOccasionsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public CafeOccasionsController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: CafeOccasions
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)); //saan ainult selle kasutaja asjad
            var res = await _bll.CafeOccasions.GetAllAsync(userId);
            return View(res);
        }

         // GET: CafeOccasions/Details/5
         public async Task<IActionResult> Details(Guid? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var cafeOccasion = await _bll.CafeOccasions.FirstOrDefaultAsync(id.Value);
             if (cafeOccasion == null)
             {
                 return NotFound();
             }

             return View(cafeOccasion);
         }

         // GET: CafeOccasions/Create
         public async Task<IActionResult> Create()
         {
             ViewData["CafeId"] = new SelectList(await _bll.Cafes.GetAllAsync(), "Id", "Id");
             ViewData["OccasionOfCafeId"] = new SelectList(await _bll.OccasionOfCafes.GetAllAsync(), "Id", "Id");
             return View();
         }

         // POST: CafeOccasions/Create
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create(App.BLL.DTO.CafeOccasion cafeOccasion)
         {
             if (ModelState.IsValid)
             {
                 cafeOccasion.Id = Guid.NewGuid();
                 _bll.CafeOccasions.Add(cafeOccasion);
                 await _bll.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", cafeOccasion.CafeId);
             // ViewData["OccasionOfCafeId"] = new SelectList(_context.OccasionOfCafes, "Id", "Id", cafeOccasion.OccasionOfCafeId);
             return View(cafeOccasion);
         }

         // GET: CafeOccasions/Edit/5
         public async Task<IActionResult> Edit(Guid? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var cafeOccasion = await _bll.CafeOccasions.FirstOrDefaultAsync(id.Value);
             if (cafeOccasion == null)
             {
                 return NotFound();
             }
             // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", cafeOccasion.CafeId);
             // ViewData["OccasionOfCafeId"] = new SelectList(_context.OccasionOfCafes, "Id", "Id", cafeOccasion.OccasionOfCafeId);
             return View(cafeOccasion);
         }

         // POST: CafeOccasions/Edit/5
         // To protect from overposting attacks, enable the specific properties you want to bind to.
         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(Guid id, [Bind("CafeId,OccasionOfCafeId,Id")] CafeOccasion cafeOccasion)
         {
             if (id != cafeOccasion.Id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _bll.CafeOccasions.Update(cafeOccasion);
                     await _bll.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!await _bll.CafeOccasions.ExistsAsync(cafeOccasion.Id))
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
             // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", cafeOccasion.CafeId);
             // ViewData["OccasionOfCafeId"] = new SelectList(_context.OccasionOfCafes, "Id", "Id", cafeOccasion.OccasionOfCafeId);
             return View(cafeOccasion);
         }

         // GET: CafeOccasions/Delete/5
         public async Task<IActionResult> Delete(Guid? id)
         {
             if (id == null)
             {
                 return NotFound();
             }

             var cafeOccasion = await _bll.CafeOccasions.FirstOrDefaultAsync(id.Value);
             if (cafeOccasion == null)
             {
                 return NotFound();
             }

             return View(cafeOccasion);
         }

         // POST: CafeOccasions/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(Guid id)
         {
             await _bll.CafeOccasions.RemoveAsync(id);
             await _bll.SaveChangesAsync();
             return RedirectToAction(nameof(Index));
         }
    }
}
