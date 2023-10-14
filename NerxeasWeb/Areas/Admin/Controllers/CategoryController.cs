using Microsoft.AspNetCore.Mvc;
using Nerxeas.DataAccess.Repository.IRepository;
using Nerxeas.Models;

namespace NerxeasWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }

        // By default, this is a HttpGet
        // Edit (11.10.2023)> I'll put the attribute anyways because I don't like "magic" tags.
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the ");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            // Another approach to retrieve an Id in a Database with Entity Framework:
            // Category? categoryFromDb2 = _db.Categories.FirstOrDefault(c => c.Id == id);
            if (categoryFromDb == null)
                return NotFound();

            return View(categoryFromDb);
        }

        // Sigue siendo un HttpPost, porque el Update lo hace con el método .Update de EntityFramework,
        // De esta forma, si el Id cambia, más que editar, también creará un objeto totalmente nuevo.
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
                return NotFound();

            return View(categoryFromDb);
        }

        [HttpPost]
        [ActionName("Delete")] // Esto es para redirigir la ruta de Delete a esta acción, a pesar de su nombre.
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);

            if (obj == null)
                return NotFound();

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
