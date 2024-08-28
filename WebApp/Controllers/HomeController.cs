using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using WebApp.Areas.Admin.ViewModels;
using WebApp.ViewModels;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWebHostEnvironment _env;

    public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
    {
        _logger = logger;
        _env = env;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult ListFiles()
    {
        return View();
    }

    public IActionResult Upload()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload(FileUploadViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var fileExtensions = new string[]
            {
                ".png", ".jpg", ".bmp", ".gif"
            };
            if (vm.File.Length > 0 && fileExtensions.Contains(Path.GetExtension(vm.File.FileName)))
            {
                var uploadDir = _env.WebRootPath;
                var fileName = Guid.NewGuid().ToString() + " " + Path.GetFileName(vm.File.FileName);
                var filePath = uploadDir + Path.DirectorySeparatorChar + "uploads" + Path.DirectorySeparatorChar + fileName;

                await using (var stream = System.IO.File.Create(filePath))
                {
                    await vm.File.CopyToAsync(stream);
                }
                return RedirectToAction(nameof(ListFiles));
            }
            ModelState.AddModelError(nameof(FileUploadViewModel.File), "This is not an image file! " + vm.File.FileName);
        }
        return View(vm);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}