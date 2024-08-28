using System.Security.Claims;
using App.BLL.DTO;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.DAL.EF;
using Microsoft.AspNetCore.Mvc;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    // [Authorize]
    public class CafesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public CafesController(UserManager<AppUser> userManager, IAppBLL bll)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Cafes
        public async Task<IActionResult> Index()
        {
            // var userId = Guid.Parse(_userManager.GetUserId(User)!); //saan ainult selle kasutaja asjad
            var res = await _bll.Cafes.GetAllAsync();
            return View(res);
        }
        
        // GET: Cafes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var cafe = await _bll.Cafes.FirstOrDefaultAsync(id.Value);
            if (cafe == null)
            {
                return NotFound();
            }
            
            return View(cafe);
        }
        
        // GET: Cafes/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Create a new Cafe instance
            var cafe = new Cafe
            {
                // Pre-set the AppUserId to the currently logged-in user's ID
                AppUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)
            };
            
            // Load cities for the CityId dropdown
            var cities = _bll.Cities.GetAllAsync().Result; // Ensure this is async and awaited properly in real use
            ViewBag.CityId = new SelectList(cities, "Id", "CityName");
            
            return View(cafe);
        }

            // ViewData["AppUserId"] = new SelectList(await _bll.AppUsers.GetAllAsync(), "Id", "Id");
            // ViewData["CityId"] = new SelectList(await _bll.Cities.GetAllAsync(), "Id", "Id");
            // var cities = await _bll.Cities.GetAllAsync();
            // ViewBag.CityId = new SelectList(cities, "Id", "CityName");
            // return View();
        
        
        // POST: Cafes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cafe cafe)
        {
            cafe.AppUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!); // Set the user ID from the current user
            cafe.Id = Guid.NewGuid(); // Assign a new Guid ID
            if (ModelState.IsValid)
            {
                _bll.Cafes.Add(cafe); // Add the cafe
                await _bll.SaveChangesAsync(); // Save the cafe
                return RedirectToAction(nameof(Index));
            }
            var cities = await _bll.Cities.GetAllAsync();
            ViewBag.CityId = new SelectList(cities, "Id", "CityName", cafe.CityId);
            return View(cafe);
        }


        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Create(Cafe cafe)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         cafe.Id = Guid.NewGuid();
        //         cafe.AppUserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        //         _bll.Cafes.Add(cafe);
        //         await _bll.SaveChangesAsync();
        //         return RedirectToAction(nameof(Index));
        //     }
        //     // ViewData["AppUserId"] = new SelectList(await _bll.AppUsers.GetAllAsync(), "Id", "Id", cafe.AppUserId);
        //     // ViewData["CityId"] = new SelectList(await _bll.Cities.GetAllAsync(), "Id", "CityName", cafe.CityId);
        //     return View(cafe);
        // }
        
        // GET: Cafes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var cafe = await _bll.Cafes.FirstOrDefaultAsync(id.Value);
            if (cafe == null)
            {
                return NotFound();
            }
            // ViewData["AppUserId"] = new SelectList(await _bll.AppUsers.GetAllAsync(), "Id", "Id", cafe.AppUserId);
            // ViewData["CityId"] = new SelectList(await _bll.Cities.GetAllAsync(), "Id", "Id", cafe.CityId);
            return View(cafe);
        }
        
        // POST: Cafes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CafeName,CafeAddress,CafeEmail,CafeTelephone,CafeWebsiteLink,CafeAverageRating,CityId,AppUserId,Id")] Cafe cafe)
        {
            if (id != cafe.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Cafes.Update(cafe);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.Cafes.ExistsAsync(cafe.Id))
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
            // ViewData["AppUserId"] = new SelectList(await _bll.AppUsers.GetAllAsync(), "Id", "Id", cafe.AppUserId);
            // ViewData["CityId"] = new SelectList(await _bll.Cities.GetAllAsync(), "Id", "Id", cafe.CityId);
            return View(cafe);
        }
        
        // GET: Cafes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var cafe = await _bll.Cafes.FirstOrDefaultAsync(id.Value);
            if (cafe == null)
            {
                return NotFound();
            }
        
            return View(cafe);
        }
        
            // POST: Cafes/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(Guid id)
            {
                await _bll.Cafes.RemoveAsync(id);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
        }
    }

