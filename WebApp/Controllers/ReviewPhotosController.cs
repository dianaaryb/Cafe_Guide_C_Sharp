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
    public class ReviewPhotosController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public ReviewPhotosController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: ReviewPhotos
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)); //saan ainult selle kasutaja asjad
            var res = await _bll.ReviewPhotos.GetAllAsync(userId);
            return View(res);
        }

        // GET: ReviewPhotos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewPhoto = await _bll.ReviewPhotos.FirstOrDefaultAsync(id.Value);
            if (reviewPhoto == null)
            {
                return NotFound();
            }
        
            return View(reviewPhoto);
        }
        
        // GET: ReviewPhotos/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ReviewId"] = new SelectList(await _bll.Reviews.GetAllAsync(), "Id", "Id");
            return View();
        }
        
        // POST: ReviewPhotos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(App.BLL.DTO.ReviewPhoto reviewPhoto)
        {
            if (ModelState.IsValid)
            {
                reviewPhoto.Id = Guid.NewGuid();
                _bll.ReviewPhotos.Add(reviewPhoto);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // ViewData["ReviewId"] = new SelectList(_context.Reviews, "Id", "Id", reviewPhoto.ReviewId);
            return View(reviewPhoto);
        }
        
        // GET: ReviewPhotos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var reviewPhoto = await _bll.ReviewPhotos.FirstOrDefaultAsync(id.Value);
            if (reviewPhoto == null)
            {
                return NotFound();
            }
            // ViewData["ReviewId"] = new SelectList(_context.Reviews, "Id", "Id", reviewPhoto.ReviewId);
            return View(reviewPhoto);
        }
        
        // POST: ReviewPhotos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReviewPhotoLink,ReviewId,Id")] App.BLL.DTO.ReviewPhoto reviewPhoto)
        {
            if (id != reviewPhoto.Id)
            {
                return NotFound();
            }
        
            if (ModelState.IsValid)
            {
                try
                {
                    _bll.ReviewPhotos.Update(reviewPhoto);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _bll.OccasionOfCafes.ExistsAsync(reviewPhoto.Id))
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
            // ViewData["ReviewId"] = new SelectList(_context.Reviews, "Id", "Id", reviewPhoto.ReviewId);
            return View(reviewPhoto);
        }
        
        // GET: ReviewPhotos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reviewPhoto = await _bll.ReviewPhotos.FirstOrDefaultAsync(id.Value);
            if (reviewPhoto == null)
            {
                return NotFound();
            }
        
            return View(reviewPhoto);
        }
        
        // POST: ReviewPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.OccasionOfCafes.RemoveAsync(id);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
