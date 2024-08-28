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
//     public class CafeOccasionsController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly ICafeOccasionRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public CafeOccasionsController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new CafeOccasionRepository(context);
//         }
//
//         // GET: Admin/CafeOccasions
//         [AllowAnonymous]
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.CafeOccasions.GetAllAsync();
//             // var appDbContext = _context.CafeOccasions.Include(c => c.Cafe).Include(c => c.OccasionOfCafe);
//             // return View(await appDbContext.ToListAsync());
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//         }
//
//         // GET: Admin/CafeOccasions/Details/5
//         [AllowAnonymous]
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafeOccasion = await _uow.CafeOccasions.FirstOrDefaultAsync(id.Value);
//             // var cafeOccasion = await _repo.FirstOrDefaultAsync(id.Value);
//             // var cafeOccasion = await _context.CafeOccasions
//             //     .Include(c => c.Cafe)
//             //     .Include(c => c.OccasionOfCafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafeOccasion == null)
//             {
//                 return NotFound();
//             }
//
//             return View(cafeOccasion);
//         }
//
//         // GET: Admin/CafeOccasions/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id");
//             ViewData["OccasionOfCafeId"] = new SelectList(await _uow.OccasionOfCafes.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/CafeOccasions/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(CafeOccasion cafeOccasion)
//         {
//             if (ModelState.IsValid)
//             {
//                 cafeOccasion.Id = Guid.NewGuid();
//                 _uow.CafeOccasions.Add(cafeOccasion);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(cafeOccasion);
//                 // _context.Add(cafeOccasion);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafeOccasion.CafeId);
//             ViewData["OccasionOfCafeId"] = new SelectList(await _uow.OccasionOfCafes.GetAllAsync(), "Id", "Id", cafeOccasion.OccasionOfCafeId);
//             return View(cafeOccasion);
//         }
//
//         // GET: Admin/CafeOccasions/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafeOccasion = await _uow.CafeOccasions.FirstOrDefaultAsync(id.Value);
//             if (cafeOccasion == null)
//             {
//                 return NotFound();
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafeOccasion.CafeId);
//             ViewData["OccasionOfCafeId"] = new SelectList(await _uow.OccasionOfCafes.GetAllAsync(), "Id", "Id", cafeOccasion.OccasionOfCafeId);
//             return View(cafeOccasion);
//         }
//
//         // POST: Admin/CafeOccasions/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("CafeId,OccasionOfCafeId,Id")] CafeOccasion cafeOccasion)
//         {
//             if (id != cafeOccasion.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.CafeOccasions.Update(cafeOccasion);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(cafeOccasion);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.CafeOccasions.ExistsAsync(cafeOccasion.Id))
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
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafeOccasion.CafeId);
//             ViewData["OccasionOfCafeId"] = new SelectList(await _uow.OccasionOfCafes.GetAllAsync(), "Id", "Id", cafeOccasion.OccasionOfCafeId);
//             return View(cafeOccasion);
//         }
//
//         // GET: Admin/CafeOccasions/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var cafeOccasion = await _uow.CafeOccasions.FirstOrDefaultAsync(id.Value);
//             // var cafeOccasion = await _context.CafeOccasions
//             //     .Include(c => c.Cafe)
//             //     .Include(c => c.OccasionOfCafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafeOccasion == null)
//             {
//                 return NotFound();
//             }
//             return View(cafeOccasion);
//         }
//
//         // POST: Admin/CafeOccasions/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var cafeOccasion = await _context.CafeOccasions.FindAsync(id);
//             // if (cafeOccasion != null)
//             // {
//             //     _context.CafeOccasions.Remove(cafeOccasion);
//             // }
//             await _uow.CafeOccasions.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }