using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication7.DBContext;

namespace WebApplication7.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogDbContext _dbContext;

        public CatalogController(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IActionResult> Index()
        {
            var catalogs = _dbContext.Catalog.ToList();
            return View(catalogs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var catalog = _dbContext.Catalog.FirstOrDefault(c => c.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            var childCatalogs = _dbContext.Catalog.Where(c => c.ParentId == id).ToList();

            ViewBag.Catalog = catalog;
            ViewBag.ChildCatalogs = childCatalogs;

            return View();
        }

        
    }
}
