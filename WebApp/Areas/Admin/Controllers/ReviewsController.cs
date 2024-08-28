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
//     public class ReviewsController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly IReviewRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public ReviewsController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new ReviewRepository(context);
//         }
//
//         // GET: Admin/Reviews
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.Reviews.GetAllAsync();
//             // var res = await _repo.GetAllAsync();
//             foreach (var review in res)
//             {
//                 review.Date = review.Date.ToLocalTime();
//             }
//             return View(res);
//             // var appDbContext = _context.Reviews.Include(r => r.AppUser).Include(r => r.Cafe);
//             // return View(await appDbContext.ToListAsync());
//         }
//
//         // GET: Admin/Reviews/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var review = await _uow.Reviews.FirstOrDefaultAsync(id.Value);
//             // var review = await _repo.FirstOrDefaultAsync(id.Value);
//             // var review = await _context.Reviews
//             //     .Include(r => r.AppUser)
//             //     .Include(r => r.Cafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (review == null)
//             {
//                 return NotFound();
//             }
//
//             return View(review);
//         }
//
//         // GET: Admin/Reviews/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id");
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/Reviews/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(Review review)
//         {
//             if (ModelState.IsValid)
//             {
//                 review.Id = Guid.NewGuid();
//                 _uow.Reviews.Add(review);
//                 await _uow.SaveChangesAsync();
//                 //the same(line below) only overall code is in AppDbContext
//                 // review.Date = review.Date.ToUniversalTime();
//                 // _repo.Add(review);
//                 // _context.Add(review);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id", review.AppUserId);
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", review.CafeId);
//             return View(review);
//         }
//
//         // GET: Admin/Reviews/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var review = await _uow.Reviews.FirstOrDefaultAsync(id.Value);
//             if (review == null)
//             {
//                 return NotFound();
//             }
//             review.Date = review.Date.ToLocalTime();
//             
//             ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id", review.AppUserId);
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", review.CafeId);
//             return View(review);
//         }
//
//         // POST: Admin/Reviews/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("Rating,Text,Date,AppUserId,CafeId,Id")] Review review)
//         {
//             if (id != review.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.Reviews.Update(review);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(review);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.Reviews.ExistsAsync(review.Id))
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
//             ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id", review.AppUserId);
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", review.CafeId);
//             return View(review);
//         }
//
//         // GET: Admin/Reviews/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var review = await _uow.Reviews.FirstOrDefaultAsync(id.Value);
//             // var review = await _context.Reviews
//             //     .Include(r => r.AppUser)
//             //     .Include(r => r.Cafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (review == null)
//             {
//                 return NotFound();
//             }
//
//             return View(review);
//         }
//
//         // POST: Admin/Reviews/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var review = await _context.Reviews.FindAsync(id);
//             // if (review != null)
//             // {
//             //     _context.Reviews.Remove(review);
//             // }
//             await _uow.Reviews.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
