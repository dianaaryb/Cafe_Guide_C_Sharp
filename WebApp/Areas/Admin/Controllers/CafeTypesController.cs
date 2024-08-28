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
//     public class CafeTypesController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly ICafeTypeRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public CafeTypesController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new CafeTypeRepository(context);
//         }
//
//         // GET: Admin/CafeTypes
//         [AllowAnonymous]
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.CafeTypes.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // var appDbContext = _context.CafeTypes.Include(c => c.Cafe).Include(c => c.TypeOfCafe);
//             // return View(await appDbContext.ToListAsync());
//         }
//
//         // GET: Admin/CafeTypes/Details/5
//         [AllowAnonymous]
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var cafeType = await _uow.CafeTypes.FirstOrDefaultAsync(id.Value);
//
//             // var cafeType = await _context.CafeTypes
//             //     .Include(c => c.Cafe)
//             //     .Include(c => c.TypeOfCafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafeType == null)
//             {
//                 return NotFound();
//             }
//             return View(cafeType);
//         }
//
//         // GET: Admin/CafeTypes/Create
//         public async Task<IActionResult> Create()
//         {
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id");
//             ViewData["TypeOfCafeId"] = new SelectList(await _uow.TypeOfCafes.GetAllAsync(), "Id", "Id");
//             return View();
//         }
//
//         // POST: Admin/CafeTypes/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(CafeType cafeType)
//         {
//             if (ModelState.IsValid)
//             {
//                 cafeType.Id = Guid.NewGuid();
//                 _uow.CafeTypes.Add(cafeType);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(cafeType);
//                 // _context.Add(cafeType);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafeType.CafeId);
//             ViewData["TypeOfCafeId"] = new SelectList(await _uow.TypeOfCafes.GetAllAsync(), "Id", "Id", cafeType.TypeOfCafeId);
//             return View(cafeType);
//         }
//
//         // GET: Admin/CafeTypes/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafeType = await _uow.CafeTypes.FirstOrDefaultAsync(id.Value);
//             if (cafeType == null)
//             {
//                 return NotFound();
//             }
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafeType.CafeId);
//             ViewData["TypeOfCafeId"] = new SelectList(await _uow.TypeOfCafes.GetAllAsync(), "Id", "Id", cafeType.TypeOfCafeId);
//             return View(cafeType);
//         }
//
//         // POST: Admin/CafeTypes/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("CafeId,TypeOfCafeId,Id")] CafeType cafeType)
//         {
//             if (id != cafeType.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.CafeTypes.Update(cafeType);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(cafeType);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.CafeTypes.ExistsAsync(cafeType.Id))
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
//             ViewData["CafeId"] = new SelectList(await _uow.Cafes.GetAllAsync(), "Id", "Id", cafeType.CafeId);
//             ViewData["TypeOfCafeId"] = new SelectList(await _uow.TypeOfCafes.GetAllAsync(), "Id", "Id", cafeType.TypeOfCafeId);
//             return View(cafeType);
//         }
//
//         // GET: Admin/CafeTypes/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var cafeType = await _uow.CafeTypes.FirstOrDefaultAsync(id.Value);
//
//             // var cafeType = await _context.CafeTypes
//             //     .Include(c => c.Cafe)
//             //     .Include(c => c.TypeOfCafe)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafeType == null)
//             {
//                 return NotFound();
//             }
//
//             return View(cafeType);
//         }
//
//         // POST: Admin/CafeTypes/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var cafeType = await _context.CafeTypes.FindAsync(id);
//             // if (cafeType != null)
//             // {
//             //     _context.CafeTypes.Remove(cafeType);
//             // }
//             await _uow.CafeTypes.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
