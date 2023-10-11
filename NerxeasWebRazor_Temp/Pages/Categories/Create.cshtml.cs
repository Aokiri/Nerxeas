using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NerxeasWebRazor_Temp.Data;
using NerxeasWebRazor_Temp.Models;

namespace NerxeasWebRazor_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        // Cuando se trabaja con Razor Pages, debes expl�citamente bindear las categor�as que utilizar�s en tus acciones.
        // Si tengo m�s de una propiedad, utilizo [BindProperties] encima del modelo (CreateModel en este caso).
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            // En Razor Pages en vez de RedirectToAction se utiliza RedirectToPage.
            return RedirectToPage("Index");
        }
    }
}
