using App.Contracts.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    // [Authorize]
    public class TypeOfCafesController : Controller
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<AppUser> _userManager;

        public TypeOfCafesController(IAppBLL bll, UserManager<AppUser> userManager)
        {
            _bll = bll;
            _userManager = userManager;
        }

        // GET: TypeOfCafes
        public async Task<IActionResult> Index()
        {
            // var userId = Guid.Parse(_userManager.GetUserId(User)); //saan ainult selle kasutaja asjad
            var res = await _bll.TypeOfCafes.GetAllAsync();
            return View(res);
        }

    // GET: TypeOfCafes/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var typeOfCafe = await _bll.TypeOfCafes.FirstOrDefaultAsync(id.Value);
        if (typeOfCafe == null)
        {
            return NotFound();
        }
    
        return View(typeOfCafe);
    }
    
    // GET: TypeOfCafes/Create
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: TypeOfCafes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(App.BLL.DTO.TypeOfCafe typeOfCafe)
    {
        typeOfCafe.Id = Guid.NewGuid();
        if (ModelState.IsValid)
        {
           
            _bll.TypeOfCafes.Add(typeOfCafe);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(typeOfCafe);
    }
    
    // GET: TypeOfCafes/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }
    
        var typeOfCafe = await _bll.TypeOfCafes.FirstOrDefaultAsync(id.Value);
        if (typeOfCafe == null)
        {
            return NotFound();
        }
        return View(typeOfCafe);
    }
    
    // POST: TypeOfCafes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("TypeOfCafeName,TypeOfCafeDescription,Id")] App.BLL.DTO.TypeOfCafe typeOfCafe)
    {
        if (id != typeOfCafe.Id)
        {
            return NotFound();
        }
    
        if (ModelState.IsValid)
        {
            try
            {
                _bll.TypeOfCafes.Update(typeOfCafe);
                await _bll.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _bll.TypeOfCafes.ExistsAsync(typeOfCafe.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(typeOfCafe);
    }
    
    // GET: TypeOfCafes/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var typeOfCafe = await _bll.TypeOfCafes.FirstOrDefaultAsync(id.Value);
        if (typeOfCafe == null)
        {
            return NotFound();
        }
    
        return View(typeOfCafe);
    }
    
    // POST: TypeOfCafes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        await _bll.TypeOfCafes.DeleteTypeAsync(id);
        await _bll.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    }
}
