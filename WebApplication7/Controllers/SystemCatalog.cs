using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication7.DBContext;

namespace WebApplication7.Controllers
{
    public class SystemCatalogController : Controller
    {
        private readonly NewCatalogDbContext _dbContext;

        public SystemCatalogController(NewCatalogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var catalogs = _dbContext.NewCatalogfromSystemcs.ToList();
            return View(catalogs);
        }

        public IActionResult ImportFromOS(string rootPath)
        {
            if (string.IsNullOrEmpty(rootPath))
            {
                // If the rootPath parameter is not provided, show the ImportFromOS view with the form.
                return View();
            }

            if (System.IO.Directory.Exists(rootPath))
            {
                var catalogsFromOS = GetCatalogsFromOS(rootPath);
                SaveCatalogsToDatabase(catalogsFromOS);
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest("Invalid path or directory does not exist.");
            }
        }

        private List<NewCatalogfromSystemcs> GetCatalogsFromOS(string rootPath)
        {
            var catalogs = new List<NewCatalogfromSystemcs>();
            ProcessDirectory(rootPath, null, catalogs);
            return catalogs;
        }

        private void ProcessDirectory(string directoryPath, NewCatalogfromSystemcs parentCatalog, List<NewCatalogfromSystemcs> catalogs)
        {
            // Получаем имя текущего каталога
            var currentDirectoryName = new DirectoryInfo(directoryPath).Name;

            // Создаем новый каталог для текущего пути
            var catalog = new NewCatalogfromSystemcs
            {
                SName = currentDirectoryName,
                SParentCatalog = parentCatalog
            };

            // Добавляем текущий каталог в список
            catalogs.Add(catalog);

            // Рекурсивно обрабатываем все подкаталоги
            var subDirectories = Directory.GetDirectories(directoryPath);
            foreach (var subDirectory in subDirectories)
            {
                ProcessDirectory(subDirectory, catalog, catalogs);
            }
        }

        private void SaveCatalogsToDatabase(List<NewCatalogfromSystemcs> catalogs)
        {
            // Удаляем все существующие записи из таблицы NewCatalogfromSystemcs.
            _dbContext.NewCatalogfromSystemcs.RemoveRange(_dbContext.NewCatalogfromSystemcs);

            // Добавляем новые записи в таблицу NewCatalogfromSystemcs.
            _dbContext.NewCatalogfromSystemcs.AddRange(catalogs);

            // Сохраняем все изменения (новые каталоги и связи между родительскими и дочерними каталогами) одним вызовом SaveChanges.
            _dbContext.SaveChanges();
        }

        public IActionResult TreeView()
        {
            var catalogs = _dbContext.NewCatalogfromSystemcs.ToList();
            return View(catalogs);
        }

        public IActionResult Details(int id)
        {
            var catalog = _dbContext.NewCatalogfromSystemcs.FirstOrDefault(c => c.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            var childCatalogs = _dbContext.NewCatalogfromSystemcs.Where(c => c.SParentId == id).ToList();

            ViewBag.Catalog = catalog;
            ViewBag.ChildCatalogs = childCatalogs;

            return View();
        }

    }
}
