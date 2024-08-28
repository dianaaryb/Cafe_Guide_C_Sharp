using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    // [Authorize]
    public class OccasionOfCafesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public OccasionOfCafesController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: OccasionOfCafes
        public async Task<IActionResult> Index()
        {
            // var userId = Guid.Parse(_userManager.GetUserId(User)); //saan ainult selle kasutaja asjad
            var res = await _bll.OccasionOfCafes.GetAllAsync();
            return View(res);
        }

        // GET: OccasionOfCafes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occasionOfCafe = await _bll.OccasionOfCafes.FirstOrDefaultAsync(id.Value);
            if (occasionOfCafe == null)
            {
                return NotFound();
            }
        
            return View(occasionOfCafe);
        }
        
        // GET: OccasionOfCafes/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }
        
        // POST: OccasionOfCafes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.BLL.DTO.OccasionOfCafe occasionOfCafe)
        {
            occasionOfCafe.Id = Guid.NewGuid();
            if (ModelState.IsValid)
            {
                _bll.OccasionOfCafes.Add(occasionOfCafe);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(occasionOfCafe);
        }
        
        // GET: OccasionOfCafes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var occasionOfCafe = await _bll.OccasionOfCafes.FirstOrDefaultAsync(id.Value);
            if (occasionOfCafe == null)
            {
                return NotFound();
            }
            return View(occasionOfCafe);
        }
        
        // POST: OccasionOfCafes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OccasionOfCafeName,OccasionOfCafeDescription,Id")] App.BLL.DTO.OccasionOfCafe occasionOfCafe)
        {
            if (id != occasionOfCafe.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.OccasionOfCafes.Update(occasionOfCafe);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.OccasionOfCafes.ExistsAsync(occasionOfCafe.Id))
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
            return View(occasionOfCafe);
        }
        
        // GET: OccasionOfCafes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occasionOfCafe = await _bll.OccasionOfCafes.FirstOrDefaultAsync(id.Value);
            if (occasionOfCafe == null)
            {
                return NotFound();
            }
        
            return View(occasionOfCafe);
        }
        
        // POST: OccasionOfCafes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.OccasionOfCafes.DeleteOccasionAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
