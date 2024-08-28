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
//     public class OccasionOfCafesController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly IOccasionOfCafeRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public OccasionOfCafesController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new OccasionOfCafeRepository(context);
//         }
//
//         // GET: Admin/OccasionOfCafes
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.OccasionOfCafes.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // return View(await _context.OccasionOfCafes.ToListAsync());
//         }
//
//         // GET: Admin/OccasionOfCafes/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             
//             // var occasionOfCafe = await _repo.FirstOrDefaultAsync(id.Value);
//             var occasionOfCafe = await _uow.OccasionOfCafes.FirstOrDefaultAsync(id.Value);
//             // var occasionOfCafe = await _context.OccasionOfCafes
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (occasionOfCafe == null)
//             {
//                 return NotFound();
//             }
//
//             return View(occasionOfCafe);
//         }
//
//         // GET: Admin/OccasionOfCafes/Create
//         public IActionResult Create()
//         {
//             return View();
//         }
//
//         // POST: Admin/OccasionOfCafes/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(OccasionOfCafe occasionOfCafe)
//         {
//             if (ModelState.IsValid)
//             {
//                 occasionOfCafe.Id = Guid.NewGuid();
//                 _uow.OccasionOfCafes.Add(occasionOfCafe);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(occasionOfCafe);
//                 // _context.Add(occasionOfCafe);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(occasionOfCafe);
//         }
//
//         // GET: Admin/OccasionOfCafes/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var occasionOfCafe = await _uow.OccasionOfCafes.FirstOrDefaultAsync(id.Value);
//             if (occasionOfCafe == null)
//             {
//                 return NotFound();
//             }
//             return View(occasionOfCafe);
//         }
//
//         // POST: Admin/OccasionOfCafes/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("OccasionOfCafeName,OccasionOfCafeDescription,Id")] OccasionOfCafe occasionOfCafe)
//         {
//             if (id != occasionOfCafe.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.OccasionOfCafes.Update(occasionOfCafe);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(occasionOfCafe);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.OccasionOfCafes.ExistsAsync(occasionOfCafe.Id))
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
//             return View(occasionOfCafe);
//         }
//
//         // GET: Admin/OccasionOfCafes/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }   
//             var occasionOfCafe = await _uow.OccasionOfCafes.FirstOrDefaultAsync(id.Value);
//             // var occasionOfCafe = await _context.OccasionOfCafes
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (occasionOfCafe == null)
//             {
//                 return NotFound();
//             }
//
//             return View(occasionOfCafe);
//         }
//
//         // POST: Admin/OccasionOfCafes/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var occasionOfCafe = await _context.OccasionOfCafes.FindAsync(id);
//             // if (occasionOfCafe != null)
//             // {
//             //     _context.OccasionOfCafes.Remove(occasionOfCafe);
//             // }
//             await _uow.OccasionOfCafes.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
