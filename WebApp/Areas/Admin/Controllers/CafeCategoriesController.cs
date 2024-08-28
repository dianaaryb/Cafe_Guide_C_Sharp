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
//     public class CafeCategoriesController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly ICafeCategoryRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public CafeCategoriesController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _context = context;
//             // _uow = new AppUOW(context);
//             // _repo = new CafeCategoryRepository(context);
//         }
//
//         // GET: Admin/CafeCategories
//         [AllowAnonymous]
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.CafeCategories.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//         }
//
//         // GET: Admin/CafeCategories/Details/5
//         [AllowAnonymous]
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafeCategory = await _uow.CafeCategories.FirstOrDefaultAsync(id.Value);
//             // var cafeCategory = await _repo.FirstOrDefaultAsync(id.Value);
//
//             // var cafeCategory = await _context.CafeCategories
//             //     .Include(c => c.Cafe)
//             //     .Include(c => c.CategoryOfCafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafeCategory == null)
//             {
//                 return NotFound();
//             }
//
//             return View(cafeCategory);
//         }
//
//         // GET: Admin/CafeCategories/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id");
//             ViewData["CategoryOfCafeId"] = new SelectList(await _uow.CategoryOfCafes.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/CafeCategories/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(CafeCategory cafeCategory)
//         {
//             if (ModelState.IsValid)
//             {
//                 cafeCategory.Id = Guid.NewGuid();
//                 _uow.CafeCategories.Add(cafeCategory);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(cafeCategory);
//                 // _context.Add(cafeCategory);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafeCategory.CafeId);
//             ViewData["CategoryOfCafeId"] = new SelectList(await _uow.CategoryOfCafes.GetAllAsync(), "Id", "Id", cafeCategory.CategoryOfCafeId);
//             return View(cafeCategory);
//         }
//
//         // GET: Admin/CafeCategories/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafeCategory = await _uow.CafeCategories.FirstOrDefaultAsync(id.Value);
//             if (cafeCategory == null)
//             {
//                 return NotFound();
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafeCategory.CafeId);
//             ViewData["CategoryOfCafeId"] = new SelectList(await _uow.CategoryOfCafes.GetAllAsync(), "Id", "Id", cafeCategory.CategoryOfCafeId);
//             return View(cafeCategory);
//         }
//
//         // POST: Admin/CafeCategories/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("CafeId,CategoryOfCafeId,Id")] CafeCategory cafeCategory)
//         {
//             if (id != cafeCategory.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.CafeCategories.Update(cafeCategory);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(cafeCategory);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.CafeCategories.ExistsAsync(cafeCategory.Id))
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
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafeCategory.CafeId);
//             ViewData["CategoryOfCafeId"] = new SelectList(await _uow.CategoryOfCafes.GetAllAsync(), "Id", "Id", cafeCategory.CategoryOfCafeId);
//             return View(cafeCategory);
//         }
//
//         // GET: Admin/CafeCategories/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var cafeCategory = await _uow.CafeCategories.FirstOrDefaultAsync(id.Value);
//             // var cafeCategory = await _context.CafeCategories
//             //     .Include(c => c.Cafe)
//             //     .Include(c => c.CategoryOfCafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafeCategory == null)
//             {
//                 return NotFound();
//             }
//
//             return View(cafeCategory);
//         }
//
//         // POST: Admin/CafeCategories/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var cafeCategory = await _context.CafeCategories.FindAsync(id);
//             // if (cafeCategory != null)
//             // {
//             //     _context.CafeCategories.Remove(cafeCategory);
//             // }
//             await _uow.CafeCategories.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
