using AdminDyanamoEnterprises.DTOs.Master;
using AdminDyanamoEnterprises.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace AdminDyanamoEnterprises.Controllers
{
    public class BannerController : Controller
    {
        private readonly IBannerRepository _repo;
        private readonly IWebHostEnvironment _env;

        public BannerController(IBannerRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        // ✅ Show all banners
        public IActionResult Banner()
        {
            /* var banners = _repo.GetBanners();
             return View(banners);*/
            return View();
        }

        // ✅ Add banner (GET)
        public IActionResult Create()
        {
            return View();
        }

        // ✅ Add banner (POST) - Multiple Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<IFormFile> ImageFile)
        {
            if (ImageFile != null && ImageFile.Count > 0)
            {
                foreach (var file in ImageFile)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    _repo.AddBanner(fileName);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = "Please select at least one image.";
            return View();
        }

        // ✅ Delete banner
        public IActionResult Delete(int id)
        {
            _repo.DeleteBanner(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index(bool? active)
        {
            var banners = _repo.GetBanners(active);
            ViewBag.Active = active;
            return View(banners);
        }

    }
}
