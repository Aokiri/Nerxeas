using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NerxeasWebRazor_Temp.Data;
using NerxeasWebRazor_Temp.Models;

namespace NerxeasWebRazor_Temp.Pages.Categories
{
    // Always bind properties, important.
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if (id != null && id != 0)
                Category = _db.Categories.Find(id);
        }

        public IActionResult OnPost(int? id)
        {
            Category? obj = _db.Categories.Find(id);

            if (obj == null)
                return NotFound();

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
