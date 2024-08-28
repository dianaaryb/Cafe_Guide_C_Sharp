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
//     public class MenuItemsController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly IMenuItemRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public MenuItemsController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new MenuItemRepository(context);
//         }
//
//         // GET: Admin/MenuItems
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.MenuItems.GetAllAsync();
//
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // var appDbContext = _context.MenuItems.Include(m => m.Menu).Include(m => m.MenuItemCategory);
//             // return View(await appDbContext.ToListAsync());
//         }
//
//         // GET: Admin/MenuItems/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var menuItem = await _uow.MenuItems.FirstOrDefaultAsync(id.Value);
//             // var menuItem = await _repo.FirstOrDefaultAsync(id.Value);
//             // var menuItem = await _context.MenuItems
//             //     .Include(m => m.Menu)
//             //     .Include(m => m.MenuItemCategory)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (menuItem == null)
//             {
//                 return NotFound();
//             }
//
//             return View(menuItem);
//         }
//
//         // GET: Admin/MenuItems/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["MenuId"] = new SelectList(await _uow.Menus.GetAllAsync(), "Id", "Id");
//             ViewData["MenuItemCategoryId"] = new SelectList(await _uow.MenuItemCategories.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/MenuItems/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(MenuItem menuItem)
//         {
//             if (ModelState.IsValid)
//             {
//                 menuItem.Id = Guid.NewGuid();
//                 _uow.MenuItems.Add(menuItem);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(menuItem);
//                 // _context.Add(menuItem);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["MenuId"] = new SelectList(await _uow.Menus.GetAllAsync(), "Id", "Id", menuItem.MenuId);
//             ViewData["MenuItemCategoryId"] = new SelectList(await _uow.MenuItemCategories.GetAllAsync(), "Id", "Id", menuItem.MenuItemCategoryId);
//             return View(menuItem);
//         }
//
//         // GET: Admin/MenuItems/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var menuItem = await _uow.MenuItems.FirstOrDefaultAsync(id.Value);
//             if (menuItem == null)
//             {
//                 return NotFound();
//             }
//             ViewData["MenuId"] = new SelectList(await _uow.Menus.GetAllAsync(), "Id", "Id", menuItem.MenuId);
//             ViewData["MenuItemCategoryId"] = new SelectList(await _uow.MenuItemCategories.GetAllAsync(), "Id", "Id", menuItem.MenuItemCategoryId);
//             return View(menuItem);
//         }
//
//         // POST: Admin/MenuItems/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("MenuItemName,MenuItemDescription,MenuItemPrice,MenuItemCategoryId,MenuId,Id")] MenuItem menuItem)
//         {
//             if (id != menuItem.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.MenuItems.Update(menuItem);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(menuItem);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.MenuItems.ExistsAsync(menuItem.Id))
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
//             ViewData["MenuId"] = new SelectList(await _uow.Menus.GetAllAsync(), "Id", "Id", menuItem.MenuId);
//             ViewData["MenuItemCategoryId"] = new SelectList(await _uow.MenuItemCategories.GetAllAsync(), "Id", "Id", menuItem.MenuItemCategoryId);
//             return View(menuItem);
//         }
//
//         // GET: Admin/MenuItems/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var menuItem = await _uow.MenuItems.FirstOrDefaultAsync(id.Value);
//
//             // var menuItem = await _context.MenuItems
//             //     .Include(m => m.Menu)
//             //     .Include(m => m.MenuItemCategory)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (menuItem == null)
//             {
//                 return NotFound();
//             }
//
//             return View(menuItem);
//         }
//
//         // POST: Admin/MenuItems/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var menuItem = await _context.MenuItems.FindAsync(id);
//             // if (menuItem != null)
//             // {
//             //     _context.MenuItems.Remove(menuItem);
//             // }
//             await _uow.MenuItems.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
