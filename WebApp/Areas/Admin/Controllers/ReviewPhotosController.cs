// using App.Contracts.DAL;
// using App.Contracts.DAL.Repositories;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using App.DAL.EF;
// using App.DAL.EF.Repositories;
// using App.Domain;
// using Microsoft.AspNetCore.Authorization;
//
// namespace WebApp.Areas.Admin.Controllers
// {
//     [Area("Admin")]
//     [Authorize(Roles = "Admin, CafeOwner, CafeVisitor")]
//     public class ReviewPhotosController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly IReviewPhotoRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public ReviewPhotosController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new ReviewPhotoRepository(context);
//         }
//
//         // GET: Admin/ReviewPhotos
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.ReviewPhotos.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // var appDbContext = _context.ReviewPhotos.Include(r => r.Review);
//             // return View(await appDbContext.ToListAsync());
//         }
//
//         // GET: Admin/ReviewPhotos/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var reviewPhoto = await _uow.ReviewPhotos.FirstOrDefaultAsync(id.Value);
//             // var reviewPhoto = await _repo.FirstOrDefaultAsync(id.Value);
//             // var reviewPhoto = await _context.ReviewPhotos
//             //     .Include(r => r.Review)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (reviewPhoto == null)
//             {
//                 return NotFound();
//             }
//
//             return View(reviewPhoto);
//         }
//
//         // GET: Admin/ReviewPhotos/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["ReviewId"] = new SelectList(await _uow.Reviews.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/ReviewPhotos/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(ReviewPhoto reviewPhoto)
//         {
//             if (ModelState.IsValid)
//             {
//                 reviewPhoto.Id = Guid.NewGuid();
//                 _uow.ReviewPhotos.Add(reviewPhoto);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(reviewPhoto);
//                 // _context.Add(reviewPhoto);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["ReviewId"] = new SelectList(await _uow.Reviews.GetAllAsync(), "Id", "Id", reviewPhoto.ReviewId);
//             return View(reviewPhoto);
//         }
//
//         // GET: Admin/ReviewPhotos/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var reviewPhoto = await _uow.ReviewPhotos.FirstOrDefaultAsync(id.Value);
//             if (reviewPhoto == null)
//             {
//                 return NotFound();
//             }
//             ViewData["ReviewId"] = new SelectList(await _uow.Reviews.GetAllAsync(), "Id", "Id", reviewPhoto.ReviewId);
//             return View(reviewPhoto);
//         }
//
//         // POST: Admin/ReviewPhotos/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("ReviewPhotoLink,ReviewId,Id")] ReviewPhoto reviewPhoto)
//         {
//             if (id != reviewPhoto.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.ReviewPhotos.Update(reviewPhoto);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(reviewPhoto);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.ReviewPhotos.ExistsAsync(reviewPhoto.Id))
//                     {
//                         return NotFound();
//                     }
//                     else
//                     {
//                         throw;
//                     }
//                 }
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["ReviewId"] = new SelectList(await _uow.Reviews.GetAllAsync(), "Id", "Id", reviewPhoto.ReviewId);
//             return View(reviewPhoto);
//         }
//
//         // GET: Admin/ReviewPhotos/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var reviewPhoto = await _uow.ReviewPhotos.FirstOrDefaultAsync(id.Value);
//             // var reviewPhoto = await _context.ReviewPhotos
//             //     .Include(r => r.Review)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (reviewPhoto == null)
//             {
//                 return NotFound();
//             }
//
//             return View(reviewPhoto);
//         }
//
//         // POST: Admin/ReviewPhotos/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var reviewPhoto = await _context.ReviewPhotos.FindAsync(id);
//             // if (reviewPhoto != null)
//             // {
//             //     _context.ReviewPhotos.Remove(reviewPhoto);
//             // }
//             await _uow.ReviewPhotos.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
