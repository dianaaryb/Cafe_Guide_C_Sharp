// using App.Contracts.DAL;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using Microsoft.EntityFrameworkCore;
// using App.Domain;
// using App.Domain.Identity;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using WebApp.Areas.Admin.ViewModels;
//
// namespace WebApp.Areas.Admin.Controllers
// {
//     [Area("Admin")] 
//     [Authorize(Roles = "Admin, CafeOwner, CafeVisitor")]
//     public class CafesController : Controller
//     {
//         // private readonly AppDbContext _context;
//         // private readonly ICafeRepository _repo;
//         private readonly IAppUnitOfWork _uow;
//         private readonly UserManager<AppUser> _userManager;
//
//         public CafesController(IAppUnitOfWork uow, UserManager<AppUser> userManager)
//         {
//             _uow = uow;
//             _userManager = userManager;
//             // _uow = new AppUOW(context);
//             // _context = context;
//             // _repo = new CafeRepository(context);
//         }
//
//         // GET: Admin/Cafes
//         public async Task<IActionResult> Index()
//         {
//             var userId = Guid.Parse(_userManager.GetUserId(User));
//             var res = await _uow.Cafes.GetAllAsync(userId);
//             // var res =
//             //     await _repo.GetAllAsync();
//             return View(res);
//         }
//
//         // GET: Admin/Cafes/Details/5
//         public async Task<IActionResult> Details(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//             var cafe = await _uow.Cafes.FirstOrDefaultAsync(id.Value);
//             // var cafe = await _repo.FirstOrDefaultAsync(id.Value);
//             // var cafe = await _context.Cafes
//             //     .Include(c => c.AppUser)
//             //     .Include(c => c.City)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafe == null)
//             {
//                 return NotFound();
//             }
//             return View(cafe);
//         }
//
//         // GET: Admin/Cafes/Create
//         public async Task<IActionResult> Create()
//         {
//             var vm = new CafeCreateEditViewModel()
//             {
//                 AppUserSelectList = new SelectList(await _uow.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.Email)),
//                 CitySelectList = new SelectList(await _uow.Cities.GetAllAsync(), nameof(City.Id), nameof(City.CityName))
//             };
//             // ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id");
//             // ViewData["CityId"] = new SelectList(await _uow.Cities.GetAllAsync(), "Id", "Id");
//             // ViewBag.foo = "zzzzzzbar1";
//             // ViewData["foo"] = "zzzzzzbar2";
//             
//             return View(vm);
//         }
//
//         // POST: Admin/Cafes/Create
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Create(CafeCreateEditViewModel vm)
//         {
//             if (ModelState.IsValid)
//             {
//                 // vm.Cafe.Id = Guid.NewGuid();
//                 _uow.Cafes.Add(vm.Cafe);
//                 await _uow.SaveChangesAsync();
//                 // _repo.Add(cafe);
//                 // await _context.SaveChangesAsync();
//                 return RedirectToAction(nameof(Index));
//             }
//             vm.AppUserSelectList = new SelectList(await _uow.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.Email), vm.Cafe.AppUserId);
//             vm.CitySelectList = new SelectList(await _uow.Cities.GetAllAsync(), nameof(City.Id), nameof(City.CityName), vm.Cafe.CityId);
//             // ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id", cafe.AppUserId);
//             // ViewData["CityId"] = new SelectList(await _uow.Cities.GetAllAsync(), "Id", "Id", cafe.CityId);
//
//             // ViewBag.foo = "bar1";
//             // ViewData["foo"] = "bar2";
//             
//             return View(vm);
//         }
//
//         // GET: Admin/Cafes/Edit/5 
//         public async Task<IActionResult> Edit(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafe = await _uow.Cafes.FirstOrDefaultAsync(id.Value);
//             if (cafe == null)
//             {
//                 return NotFound();
//             }
//
//             var vm = new CafeCreateEditViewModel()
//             {
//                 Cafe = cafe,
//                 AppUserSelectList = new SelectList(await _uow.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.Email), cafe.AppUserId),
//                 CitySelectList = new SelectList(await _uow.Cities.GetAllAsync(), nameof(City.Id), nameof(City.CityName), cafe.CityId)
//             };
//             // ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id", cafe.AppUserId);
//             // ViewData["CityId"] = new SelectList(await _uow.Cities.GetAllAsync(), "Id", "Id", cafe.CityId);
//             return View(vm);
//         }
//
//         // POST: Admin/Cafes/Edit/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to.
//         // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//         [HttpPost]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> Edit(Guid id, CafeCreateEditViewModel vm)
//         {
//             if (id != vm.Cafe.Id)
//             {
//                 return NotFound();
//             }
//
//             if (ModelState.IsValid)
//             {
//                 try
//                 {
//                     _uow.Cafes.Update(vm.Cafe);
//                     await _uow.SaveChangesAsync();
//                     // _context.Update(cafe);
//                     // await _context.SaveChangesAsync();
//                 }
//                 catch (DbUpdateConcurrencyException)
//                 {
//                     if (!await _uow.Cafes.ExistsAsync(vm.Cafe.Id))
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
//             vm.AppUserSelectList = new SelectList(await _uow.AppUsers.GetAllAsync(), nameof(AppUser.Id), nameof(AppUser.Email), vm.Cafe.AppUserId);
//             vm.CitySelectList = new SelectList(await _uow.Cities.GetAllAsync(), nameof(City.Id), nameof(City.CityName), vm.Cafe.CityId);
//             // ViewData["AppUserId"] = new SelectList(await _uow.AppUsers.GetAllAsync(), "Id", "Id", cafe.AppUserId);
//             // ViewData["CityId"] = new SelectList(await _uow.Cities.GetAllAsync(), "Id", "Id", cafe.CityId);
//             return View(vm);
//         }
//
//         // GET: Admin/Cafes/Delete/5
//         public async Task<IActionResult> Delete(Guid? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var cafe = await _uow.Cafes.FirstOrDefaultAsync(id.Value);
//             // var cafe = await _context.Cafes
//             //     .Include(c => c.AppUser)
//             //     .Include(c => c.City)
//             //     .FirstOrDefaultAsync(m => m.Id == id);
//             if (cafe == null)
//             {
//                 return NotFound();
//             }
//
//             return View(cafe);
//         }
//
//         // POST: Admin/Cafes/Delete/5
//         [HttpPost, ActionName("Delete")]
//         [ValidateAntiForgeryToken]
//         public async Task<IActionResult> DeleteConfirmed(Guid id)
//         {
//             // var cafe = await _context.Cafes.FindAsync(id);
//             // if (cafe != null)
//             // {
//             //     _context.Cafes.Remove(cafe);
//             // }
//             await _uow.Cafes.RemoveAsync(id);
//             await _uow.SaveChangesAsync();
//             return RedirectToAction(nameof(Index));
//         }
//     }
// }
