using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    // [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public ReviewsController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            // var userId = Guid.Parse(_userManager.GetUserId(User)!); //saan ainult selle kasutaja asjad
            var res = await _bll.Reviews.GetAllAsync();
            return View(res);
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _bll.Reviews.FirstOrDefaultAsync(id.Value);
            if (review == null)
            {
                return NotFound();
            }
        
            return View(review);
        }
        
        // GET: Reviews/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Create a new Cafe instance
            var review = new App.BLL.DTO.Review
            {
                // Pre-set the AppUserId to the currently logged-in user's ID
                AppUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                Date = DateTime.UtcNow
                    .AddTicks(-(DateTime.UtcNow.Ticks % TimeSpan.TicksPerMinute)) // Set the def // Set the default date to the current date and time in UTC with milliseconds removed
            };
            // ViewData["AppUserId"] = new SelectList(await _bll.AppUsers.GetAllAsync(), "Id", "FirstName");
            ViewBag.CafeId = new SelectList(_bll.Cafes.GetAllAsync().Result, "Id", "CafeName");
            return View(review);
        }
        
        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.BLL.DTO.Review review)
        {
            review.AppUserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            review.Id = Guid.NewGuid();
            review.Date = DateTime.UtcNow
                .AddTicks(-(DateTime.UtcNow.Ticks % TimeSpan.TicksPerMinute));// Set the def
            if (ModelState.IsValid)
            {
                _bll.Reviews.Add(review);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", review.AppUserId);
            ViewBag.CafeId = new SelectList(await _bll.Cafes.GetAllAsync(), "Id", "CafeName", review.CafeId);
            return View(review);
        }
        
        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var review = await _bll.Reviews.FirstOrDefaultAsync(id.Value);
            if (review == null)
            {
                return NotFound();
            }
            // ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", review.AppUserId);
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", review.CafeId);
            return View(review);
        }
        
        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Rating,Text,Date,AppUserId,CafeId,Id")] App.BLL.DTO.Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Reviews.Update(review);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.Reviews.ExistsAsync(review.Id))
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
            // ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "FirstName", review.AppUserId);
            // ViewData["CafeId"] = new SelectList(_context.Cafes, "Id", "Id", review.CafeId);
            return View(review);
        }
        
        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _bll.Reviews.FirstOrDefaultAsync(id.Value);
            if (review == null)
            {
                return NotFound();
            }
        
            return View(review);
        }
        
        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Reviews.DeleteReviewAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
