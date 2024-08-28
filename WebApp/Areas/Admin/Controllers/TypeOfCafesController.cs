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
//     public class TypeOfCafesController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly ITypeOfCafeRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public TypeOfCafesController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new TypeOfCafeRepository(context);
//         }
//
//         // GET: Admin/TypeOfCafes
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.TypeOfCafes.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // return View(await _context.TypeOfCafes.ToListAsync());
//         }
//
//         // GET: Admin/TypeOfCafes/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             
//             // var typeOfCafe = await _repo.FirstOrDefaultAsync(id.Value);
//             var typeOfCafe = await _uow.TypeOfCafes.FirstOrDefaultAsync(id.Value);
//             // var typeOfCafe = await _context.TypeOfCafes
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (typeOfCafe == null)
//             {
//                 return NotFound();
//             }
//
//             return View(typeOfCafe);
//         }
//
//         // GET: Admin/TypeOfCafes/Create
//         public IActionResult Create()
//         {
//             return View();
//         }
//
//         // POST: Admin/TypeOfCafes/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(TypeOfCafe typeOfCafe)
//         {
//             if (ModelState.IsValid)
//             {
//                 typeOfCafe.Id = Guid.NewGuid();
//                 _uow.TypeOfCafes.Add(typeOfCafe);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(typeOfCafe);
//                 // _context.Add(typeOfCafe);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(typeOfCafe);
//         }
//
//         // GET: Admin/TypeOfCafes/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var typeOfCafe = await _uow.TypeOfCafes.FirstOrDefaultAsync(id.Value);
//             if (typeOfCafe == null)
//             {
//                 return NotFound();
//             }
//             return View(typeOfCafe);
//         }
//
//         // POST: Admin/TypeOfCafes/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("TypeOfCafeName,TypeOfCafeDescription,Id")] TypeOfCafe typeOfCafe)
//         {
//             if (id != typeOfCafe.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.TypeOfCafes.Update(typeOfCafe);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(typeOfCafe);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.TypeOfCafes.ExistsAsync(typeOfCafe.Id))
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
//             return View(typeOfCafe);
//         }
//
//         // GET: Admin/TypeOfCafes/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var typeOfCafe = await _uow.TypeOfCafes.FirstOrDefaultAsync(id.Value);
//             // var typeOfCafe = await _context.TypeOfCafes
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (typeOfCafe == null)
//             {
//                 return NotFound();
//             }
//
//             return View(typeOfCafe);
//         }
//
//         // POST: Admin/TypeOfCafes/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var typeOfCafe = await _context.TypeOfCafes.FindAsync(id);
//             // if (typeOfCafe != null)
//             // {
//             //     _context.TypeOfCafes.Remove(typeOfCafe);
//             // }
//             await _uow.TypeOfCafes.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
