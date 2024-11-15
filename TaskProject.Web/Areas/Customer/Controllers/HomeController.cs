using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskProject.DataAccess.Handlers;
using TaskProject.Entities.Models;
using TaskProject.Entities.Repositories.Contract;
using TaskProject.Entities.ViewModels;

namespace TaskProject.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public IActionResult Index(DatabaseType dbType)
        //{
        //    // Create the DbContext based on the selected DatabaseType
        //    using var dbContext = _dbContextFactory.CreateDbContext(dbType);

        //    // Access data based on the context type
        //    //var data = dbContext.Set<Contact>().ToList();

        //    // Your logic here

        //    return View();
        //}

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            string userId = claim.Value;

            ApplicationUserVM applicationUserVM = new ApplicationUserVM()
            {
                ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == userId)
            };

            return View(applicationUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUserVM applicationUserVM)
        {
            if(ModelState.IsValid)
            {
                _unitOfWork.ApplicationUser.Update(applicationUserVM.ApplicationUser);
                _unitOfWork.Complete();
                TempData["Update"] = "Data Has Updated Successfully";
                return RedirectToAction("Index");
            }

            return View(applicationUserVM.ApplicationUser);
        }
    }
}
