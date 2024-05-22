using Agency_Web_App.DAL;
using Agency_Web_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Agency_Web_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortofolioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PortofolioController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public IActionResult Index()
        {
            return View(_context.Portofolios.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]


        public IActionResult Create(Portofolio portofolio)
        {

            if (!ModelState.IsValid)
            {
                return View();

            }
            string fileName = Guid.NewGuid() + portofolio.ImgFile.FileName;
            string path = _environment.WebRootPath + @"\Upload\Portofolio\";

            using (FileStream stream = new FileStream(path + fileName, FileMode.Create))
            {
                portofolio.ImgFile.CopyTo(stream);
            }
            portofolio.ImgUrl = fileName;
            _context.Portofolios.Add(portofolio);
            _context.SaveChanges();


            return RedirectToAction("Index");

        }
        public IActionResult Update(int id)
        {

            Portofolio portofolio = _context.Portofolios.FirstOrDefault(x => x.Id == id);

            if (portofolio == null)
            {
                return RedirectToAction("Index");
            }


            return View(portofolio);

        }
        [HttpPost]

        public IActionResult Update( Portofolio newportofolio)

        {
            Portofolio  oldportofolio = _context.Portofolios.FirstOrDefault(x => x.Id == newportofolio.Id);

            if (!ModelState.IsValid)
            {
                return View(oldportofolio);

            }
            if (newportofolio.ImgFile != null)
            {
                string fileName = Guid.NewGuid() + newportofolio.ImgFile.FileName;
                string path = _environment.WebRootPath + @"\Upload\Portofolio\";

                using (FileStream stream = new FileStream(path + fileName, FileMode.Create))
                {
                    newportofolio.ImgFile.CopyTo(stream);
                }
                oldportofolio.ImgUrl = fileName;
            }

            oldportofolio.FullName = newportofolio.FullName;
            oldportofolio.WebsiteType = newportofolio.WebsiteType;
       
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Portofolio portofolio = _context.Portofolios.FirstOrDefault(x => x.Id == id);
            if (portofolio == null) return NotFound();


            string imagePath = Path.Combine(_environment.WebRootPath, "Upload", "Portofolio", portofolio.ImgUrl);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Portofolios.Remove(portofolio);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
