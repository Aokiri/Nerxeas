using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nerxeas.DataAccess.Repository.IRepository;
using Nerxeas.Models;
using Nerxeas.Utility;

namespace NerxeasWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        // Stands for "Update + Insert". If ID is present, will Update/Edit; else Insert/Create.
        {
            if (id == null || id == 0)
            {
                // Create 
                return View(new Company());
            }
            else
            {
                // Update functionality
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {
                if (companyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(companyObj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Company created successfully";

                return RedirectToAction("Index");
            }
            else // Handling some exceptions. If something is not populated, it returns back the object.
            {
                return View(companyObj);
            }
        }

        // As .NET Controller supports buildt-in APIs,
        // we just have to call them instead of building an entire API for anything.
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting the Company" });
            }

            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Company deleted successfully" });
        }

        #endregion
    }
}
