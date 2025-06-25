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

        // ✅ Show banners with filter
        public IActionResult Banner(string type = null)
        {
            var banners = _repo.GetBanners(type);
            ViewBag.BannerType = type;
            ViewBag.BannerTypes = new List<string> { "Home", "Category", "SubCategory", "Product", "Fabric", "Pattern", "Colors" };
            return View(banners);
        }

        // ✅ Add banner (GET)
        public IActionResult Create()
        {
            ViewBag.BannerTypes = new List<string> { "Home", "Category", "SubCategory", "Product", "Fabric", "Pattern", "Colors" };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<IFormFile> ImageFile, string bannerType)
        {
            if (ImageFile != null && ImageFile.Count > 0 && !string.IsNullOrEmpty(bannerType))
            {
                foreach (var file in ImageFile)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string savePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    _repo.AddBanner(fileName, bannerType);
                }

                return RedirectToAction("Banner","Banner", new { type = bannerType });
            }

            TempData["Error"] = "Please select image(s) and banner type.";
            return RedirectToAction("Index");
        }

        // ✅ Delete banner
        public IActionResult Delete(int id)
        {
            _repo.DeleteBanner(id);
            return RedirectToAction("Banner", "Banner");
        }
    }
}