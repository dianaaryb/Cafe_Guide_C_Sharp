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
//     public class CafePhotosController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly ICafePhotoRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public CafePhotosController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new CafePhotoRepository(context);
//         }
//
//         // GET: Admin/CafePhotos
//         [AllowAnonymous]
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.CafePhotos.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // var appDbContext = _context.CafePhotos.Include(c => c.Cafe);
//             // return View(await appDbContext.ToListAsync());
//         }
//
//         // GET: Admin/CafePhotos/Details/5
//         [AllowAnonymous]
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafePhoto = await _uow.CafePhotos.FirstOrDefaultAsync(id.Value);
//             // var cafePhoto = await _repo.FirstOrDefaultAsync(id.Value);
//             // var cafePhoto = await _context.CafePhotos
//             //     .Include(c => c.Cafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafePhoto == null)
//             {
//                 return NotFound();
//             }
//             return View(cafePhoto);
//         }
//
//         // GET: Admin/CafePhotos/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/CafePhotos/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(CafePhoto cafePhoto)
//         {
//             if (ModelState.IsValid)
//             {
//                 cafePhoto.Id = Guid.NewGuid();
//                 _uow.CafePhotos.Add(cafePhoto);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(cafePhoto);
//                 // _context.Add(cafePhoto);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafePhoto.CafeId);
//             return View(cafePhoto);
//         }
//
//         // GET: Admin/CafePhotos/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafePhoto = await _uow.CafePhotos.FirstOrDefaultAsync(id.Value);
//             if (cafePhoto == null)
//             {
//                 return NotFound();
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafePhoto.CafeId);
//             return View(cafePhoto);
//         }
//
//         // POST: Admin/CafePhotos/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("PhotoLink,CafeId,Id")] CafePhoto cafePhoto)
//         {
//             if (id != cafePhoto.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.CafePhotos.Update(cafePhoto);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(cafePhoto);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.CafePhotos.ExistsAsync(cafePhoto.Id))
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
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafePhoto.CafeId);
//             return View(cafePhoto);
//         }
//
//         // GET: Admin/CafePhotos/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafePhoto = await _uow.CafePhotos.FirstOrDefaultAsync(id.Value);
//
//             // var cafePhoto = await _context.CafePhotos
//             //     .Include(c => c.Cafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafePhoto == null)
//             {
//                 return NotFound();
//             }
//
//             return View(cafePhoto);
//         }
//
//         // POST: Admin/CafePhotos/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var cafePhoto = await _context.CafePhotos.FindAsync(id);
//             // if (cafePhoto != null)
//             // {
//             //     _context.CafePhotos.Remove(cafePhoto);
//             // }
//             await _uow.CafePhotos.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
