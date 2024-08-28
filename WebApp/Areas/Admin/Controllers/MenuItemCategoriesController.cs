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
//     public class MenuItemCategoriesController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly IMenuItemCategoryRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public MenuItemCategoriesController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _context = context;
//             // _repo = new MenuItemCategoryRepository(context);
//             // _uow = new AppUOW(context);
//         }
//
//         // GET: Admin/MenuItemCategories
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.MenuItemCategories.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // return View(await _context.MenuItemCategories.ToListAsync());
//         }
//
//         // GET: Admin/MenuItemCategories/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var menuItemCategory = await _uow.MenuItemCategories.FirstOrDefaultAsync(id.Value);
//             // var menuItemCategory = await _repo.FirstOrDefaultAsync(id.Value);
//             // var menuItemCategory = await _context.MenuItemCategories
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (menuItemCategory == null)
//             {
//                 return NotFound();
//             }
//
//             return View(menuItemCategory);
//         }
//
//         // GET: Admin/MenuItemCategories/Create
//         public IActionResult Create()
//         {
//             return View();
//         }
//
//         // POST: Admin/MenuItemCategories/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(MenuItemCategory menuItemCategory)
//         {
//             if (ModelState.IsValid)
//             {
//                 menuItemCategory.Id = Guid.NewGuid();
//                 _uow.MenuItemCategories.Add(menuItemCategory);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(menuItemCategory);
//                 // _context.Add(menuItemCategory);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(menuItemCategory);
//         }
//
//         // GET: Admin/MenuItemCategories/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var menuItemCategory = await _uow.MenuItemCategories.FirstOrDefaultAsync(id.Value);
//             if (menuItemCategory == null)
//             {
//                 return NotFound();
//             }
//             return View(menuItemCategory);
//         }
//
//         // POST: Admin/MenuItemCategories/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("MenuItemCategoryName,Id")] MenuItemCategory menuItemCategory)
//         {
//             if (id != menuItemCategory.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.MenuItemCategories.Update(menuItemCategory);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(menuItemCategory);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.MenuItemCategories.ExistsAsync(menuItemCategory.Id))
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
//             return View(menuItemCategory);
//         }
//
//         // GET: Admin/MenuItemCategories/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var menuItemCategory = await _uow.MenuItemCategories.FirstOrDefaultAsync(id.Value);
//             // var menuItemCategory = await _context.MenuItemCategories
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (menuItemCategory == null)
//             {
//                 return NotFound();
//             }
//
//             return View(menuItemCategory);
//         }
//
//         // POST: Admin/MenuItemCategories/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var menuItemCategory = await _context.MenuItemCategories.FindAsync(id);
//             // if (menuItemCategory != null)
//             // {
//             //     _context.MenuItemCategories.Remove(menuItemCategory);
//             // }
//             await _uow.MenuItemCategories.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
