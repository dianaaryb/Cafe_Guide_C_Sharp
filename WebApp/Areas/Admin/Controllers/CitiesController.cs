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
//     public class CitiesController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly ICityRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//
//         public CitiesController(IAppUnitOfWork uow)
//         {
//             _uow = uow;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new CityRepository(context);
//         }
//
//         // GET: Admin/Cities
//         [AllowAnonymous]
//         public async Task<IActionResult> Index()
//         {
//             var res = await _uow.Cities.GetAllAsync();
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//             // return View(await _context.Cities.ToListAsync());
//         }
//
//         // GET: Admin/Cities/Details/5
//         [AllowAnonymous]
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             // var city = await _repo.FirstOrDefaultAsync(id.Value);
//             var city = await _uow.Cities.FirstOrDefaultAsync(id.Value);
//             // var city = await _context.Cities
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (city == null)
//             {
//                 return NotFound();
//             }
//
//             return View(city);
//         }
//
//         // GET: Admin/Cities/Create
//         public IActionResult Create()
//         {
//             return View();
//         }
//
//         // POST: Admin/Cities/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(City city)
//         {
//             if (ModelState.IsValid)
//             {
//                 city.Id = Guid.NewGuid();
//                 _uow.Cities.Add(city);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(city);
//                 // _context.Add(city);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             return View(city);
//         }
//
//         // GET: Admin/Cities/Edit/5
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var city = await _uow.Cities.FirstOrDefaultAsync(id.Value);
//             if (city == null)
//             {
//                 return NotFound();
//             }
//             return View(city);
//         }
//
//         // POST: Admin/Cities/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, [Bind("CityName,Id")] City city)
//         {
//             if (id != city.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.Cities.Update(city);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(city);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.Cities.ExistsAsync(city.Id))
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
//             return View(city);
//         }
//
//         // GET: Admin/Cities/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var city = await _uow.Cities.FirstOrDefaultAsync(id.Value);
//             // var city = await _context.Cities
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (city == null)
//             {
//                 return NotFound();
//             }
//
//             return View(city);
//         }
//
//         // POST: Admin/Cities/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var city = await _context.Cities.FindAsync(id);
//             // if (city != null)
//             // {
//             //     _context.Cities.Remove(city);
//             // }
//             await _uow.Cities.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
