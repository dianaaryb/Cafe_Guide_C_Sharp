// using App.Contracts.DAL;
// using App.Contracts.DAL.Repositories;
// using Microsoft.AspNetCore.Mvc;
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
//     public class CategoryOfCafesController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly ICategoryOfCafeRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public CategoryOfCafesController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new CategoryOfCafeRepository(context);
//         }
//
//         // GET: Admin/CategoryOfCafes
//         [AllowAnonymous]
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.CategoryOfCafes.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // return View(await _context.CategoryOfCafes.ToListAsync());
//         }
//
//         // GET: Admin/CategoryOfCafes/Details/5
//         [AllowAnonymous]
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             // var categoryOfCafe = await _repo.FirstOrDefaultAsync(id.Value);
//             var categoryOfCafe = await _uow.CategoryOfCafes.FirstOrDefaultAsync(id.Value);
//             // var categoryOfCafe = await _context.CategoryOfCafes
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (categoryOfCafe == null)
//             {
//                 return NotFound();
//             }
//
//             return View(categoryOfCafe);
//         }
//
//         // GET: Admin/CategoryOfCafes/Create
//         public IActionResult Create()
//         {
//             return View();
//         }
//
//         // POST: Admin/CategoryOfCafes/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(CategoryOfCafe categoryOfCafe)
//         {
//             if (ModelState.IsValid)
//             {
//                 categoryOfCafe.Id = Guid.NewGuid();
//                 _uow.CategoryOfCafes.Add(categoryOfCafe);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(categoryOfCafe);
//                 // _context.Add(categoryOfCafe);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(categoryOfCafe);
//         }
//
//         // GET: Admin/CategoryOfCafes/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var categoryOfCafe = await _uow.CategoryOfCafes.FirstOrDefaultAsync(id.Value);
//             if (categoryOfCafe == null)
//             {
//                 return NotFound();
//             }
//             return View(categoryOfCafe);
//         }
//
//         // POST: Admin/CategoryOfCafes/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("CategoryOfCafeName,CategoryOfCafeDescription,Id")] CategoryOfCafe categoryOfCafe)
//         {
//             if (id != categoryOfCafe.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.CategoryOfCafes.Update(categoryOfCafe);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(categoryOfCafe);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.CategoryOfCafes.ExistsAsync(categoryOfCafe.Id))
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
//             return View(categoryOfCafe);
//         }
//
//         // GET: Admin/CategoryOfCafes/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var categoryOfCafe = await _uow.CategoryOfCafes.FirstOrDefaultAsync(id.Value);
//             // var categoryOfCafe = await _context.CategoryOfCafes
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (categoryOfCafe == null)
//             {
//                 return NotFound();
//             }
//
//             return View(categoryOfCafe);
//         }
//
//         // POST: Admin/CategoryOfCafes/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var categoryOfCafe = await _context.CategoryOfCafes.FindAsync(id);
//             // if (categoryOfCafe != null)
//             // {
//             //     _context.CategoryOfCafes.Remove(categoryOfCafe);
//             // }
//             await _uow.CategoryOfCafes.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
