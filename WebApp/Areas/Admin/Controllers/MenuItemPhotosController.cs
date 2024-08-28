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
//     public class MenuItemPhotosController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly IMenuItemPhotoRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public MenuItemPhotosController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new MenuItemPhotoRepository(context);
//         }
//
//         // GET: Admin/MenuItemPhotos
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.MenuItemPhotos.GetAllAsync();
//
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // var appDbContext = _context.MenuItemPhotos.Include(m => m.MenuItem);
//             // return View(await appDbContext.ToListAsync());
//         }
//
//         // GET: Admin/MenuItemPhotos/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             // var menuItemPhoto = await _repo.FirstOrDefaultAsync(id.Value);
//             var menuItemPhoto = await _uow.MenuItemPhotos.FirstOrDefaultAsync(id.Value);
//             // var menuItemPhoto = await _context.MenuItemPhotos
//             //     .Include(m => m.MenuItem)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (menuItemPhoto == null)
//             {
//                 return NotFound();
//             }
//
//             return View(menuItemPhoto);
//         }
//
//         // GET: Admin/MenuItemPhotos/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["MenuItemId"] = new SelectList(await _uow.MenuItems.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/MenuItemPhotos/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(MenuItemPhoto menuItemPhoto)
//         {
//             if (ModelState.IsValid)
//             {
//                 menuItemPhoto.Id = Guid.NewGuid();
//                 _uow.MenuItemPhotos.Add(menuItemPhoto);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(menuItemPhoto);
//                 // _context.Add(menuItemPhoto);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["MenuItemId"] = new SelectList(await _uow.MenuItems.GetAllAsync(), "Id", "Id", menuItemPhoto.MenuItemId);
//             return View(menuItemPhoto);
//         }
//
//         // GET: Admin/MenuItemPhotos/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var menuItemPhoto = await _uow.MenuItemPhotos.FirstOrDefaultAsync(id.Value);
//             if (menuItemPhoto == null)
//             {
//                 return NotFound();
//             }
//             ViewData["MenuItemId"] = new SelectList(await _uow.MenuItems.GetAllAsync(), "Id", "Id", menuItemPhoto.MenuItemId);
//             return View(menuItemPhoto);
//         }
//
//         // POST: Admin/MenuItemPhotos/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("MenuItemPhotoLink,MenuItemId,Id")] MenuItemPhoto menuItemPhoto)
//         {
//             if (id != menuItemPhoto.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.MenuItemPhotos.Update(menuItemPhoto);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(menuItemPhoto);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.MenuItemPhotos.ExistsAsync(menuItemPhoto.Id))
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
//             ViewData["MenuItemId"] = new SelectList(await _uow.MenuItems.GetAllAsync(), "Id", "Id", menuItemPhoto.MenuItemId);
//             return View(menuItemPhoto);
//         }
//
//         // GET: Admin/MenuItemPhotos/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var menuItemPhoto = await _uow.MenuItemPhotos.FirstOrDefaultAsync(id.Value);
//             // var menuItemPhoto = await _context.MenuItemPhotos
//             //     .Include(m => m.MenuItem)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (menuItemPhoto == null)
//             {
//                 return NotFound();
//             }
//
//             return View(menuItemPhoto);
//         }
//
//         // POST: Admin/MenuItemPhotos/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var menuItemPhoto = await _context.MenuItemPhotos.FindAsync(id);
//             // if (menuItemPhoto != null)
//             // {
//             //     _context.MenuItemPhotos.Remove(menuItemPhoto);
//             // }
//             await _uow.MenuItemPhotos.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
