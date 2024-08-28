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
//     public class FavouritesController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly IFavouriteRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public FavouritesController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _context = context;
//             // _repo = new FavouriteRepository(context);
//             // _uow = new AppUOW(context);
//         }
//
//         // GET: Admin/Favourites
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.Favourites.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // var appDbContext = _context.Favourites.Include(f => f.AppUser).Include(f => f.Cafe);
//             // return View(await appDbContext.ToListAsync());
//         }
//
//         // GET: Admin/Favourites/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var favourite = await _uow.Favourites.FirstOrDefaultAsync(id.Value);
//             // var favourite = await _repo.FirstOrDefaultAsync(id.Value);
//             // var favourite = await _context.Favourites
//             //     .Include(f => f.AppUser)
//             //     .Include(f => f.Cafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (favourite == null)
//             {
//                 return NotFound();
//             }
//
//             return View(favourite);
//         }
//
//         // GET: Admin/Favourites/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id");
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/Favourites/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(Favourite favourite)
//         {
//             if (ModelState.IsValid)
//             {
//                 favourite.Id = Guid.NewGuid();
//                 _uow.Favourites.Add(favourite);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(favourite);
//                 // _context.Add(favourite);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id", favourite.AppUserId);
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", favourite.CafeId);
//             return View(favourite);
//         }
//
//         // GET: Admin/Favourites/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var favourite = await _uow.Favourites.FirstOrDefaultAsync(id.Value);
//             if (favourite == null)
//             {
//                 return NotFound();
//             }
//             ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id", favourite.AppUserId);
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", favourite.CafeId);
//             return View(favourite);
//         }
//
//         // POST: Admin/Favourites/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("AppUserId,CafeId,Id")] Favourite favourite)
//         {
//             if (id != favourite.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.Favourites.Update(favourite);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(favourite);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.Favourites.ExistsAsync(favourite.Id))
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
//             ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id", favourite.AppUserId);
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", favourite.CafeId);
//             return View(favourite);
//         }
//
//         // GET: Admin/Favourites/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var favourite = await _uow.Favourites.FirstOrDefaultAsync(id.Value);
//             // var favourite = await _context.Favourites
//             //     .Include(f => f.AppUser)
//             //     .Include(f => f.Cafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (favourite == null)
//             {
//                 return NotFound();
//             }
//
//             return View(favourite);
//         }
//
//         // POST: Admin/Favourites/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var favourite = await _context.Favourites.FindAsync(id);
//             // if (favourite != null)
//             // {
//             //     _context.Favourites.Remove(favourite);
//             // }
//             await _uow.Favourites.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
