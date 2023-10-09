using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NerxeasWebRazor_Temp.Data;
using NerxeasWebRazor_Temp.Models;

namespace NerxeasWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        // Los métodos en Razor Pages tienen un syntax de "OnMethod", donde Method puede ser cualquier verbo HTTP.
        // No es necesario justificar el return ni el return type porque la Razor Page automatiza ese proceso en su view.
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
