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
//     public class MenusController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly IMenuRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public MenusController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new MenuRepository(context);
//         }
//
//         // GET: Admin/Menus
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.Menus.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // var appDbContext = _context.Menus.Include(m => m.Cafe);
//             // return View(await appDbContext.ToListAsync());
//         }
//
//         // GET: Admin/Menus/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             
//             // var menu = await _repo.FirstOrDefaultAsync(id.Value);
//             var menu = await _uow.Menus.FirstOrDefaultAsync(id.Value);
//
//             // var menu = await _context.Menus
//             //     .Include(m => m.Cafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (menu == null)
//             {
//                 return NotFound();
//             }
//             return View(menu);
//         }
//
//         // GET: Admin/Menus/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/Menus/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(Menu menu)
//         {
//             if (ModelState.IsValid)
//             {
//                 menu.Id = Guid.NewGuid();
//                 _uow.Menus.Add(menu);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(menu);
//                 // _context.Add(menu);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", menu.CafeId);
//             return View(menu);
//         }
//
//         // GET: Admin/Menus/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var menu = await _uow.Menus.FirstOrDefaultAsync(id.Value);
//             if (menu == null)
//             {
//                 return NotFound();
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", menu.CafeId);
//             return View(menu);
//         }
//
//         // POST: Admin/Menus/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("CafeId,Id")] Menu menu)
//         {
//             if (id != menu.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.Menus.Update(menu);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(menu);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.Menus.ExistsAsync(menu.Id))
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
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", menu.CafeId);
//             return View(menu);
//         }
//
//         // GET: Admin/Menus/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var menu = await _uow.Menus.FirstOrDefaultAsync(id.Value);
//             // var menu = await _context.Menus
//             //     .Include(m => m.Cafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (menu == null)
//             {
//                 return NotFound();
//             }
//
//             return View(menu);
//         }
//
//         // POST: Admin/Menus/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var menu = await _context.Menus.FindAsync(id);
//             // if (menu != null)
//             // {
//             //     _context.Menus.Remove(menu);
//             // }
//             await _uow.Menus.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
