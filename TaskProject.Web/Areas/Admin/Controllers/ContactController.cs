using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TaskProject.DataAccess.Handlers;
using TaskProject.Entities.Models;
using TaskProject.Entities.Repositories.Contract;

namespace TaskProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbContextFactory _dbContextFactory;
        public ContactController(IUnitOfWork unitOfWork, DbContextFactory dbContextFactory)
        {
            _unitOfWork = unitOfWork;
            _dbContextFactory = dbContextFactory;
        }

        //public IActionResult Index()
        //{
        //    var contacts = _unitOfWork.Contact.GetAll();
        //    return View(contacts);
        //}


        public IActionResult Index(DatabaseType dbType)
        {
            using var dbContext = _dbContextFactory.CreateDbContext(dbType);

            var data = dbContext.Set<Contact>().ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Contact.Add(contact);
                _unitOfWork.Complete();
                TempData["Create"] = "Data Has Created Successfully";
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == 0 | id == null)
            {
                NotFound();
            }

            var contact = _unitOfWork.Contact.GetFirstOrDefault(c => c.Id == id);

            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Contact.Update(contact);
                    _unitOfWork.Complete();

                    TempData["Update"] = "Data Has Updated Successfully";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty, "This record was modified by another user.");
                    return View(contact);
                }
            }

            return View(contact);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == 0 | id == null)
            {
                NotFound();
            }

            var contact = _unitOfWork.Contact.GetFirstOrDefault(c => c.Id == id);

            return View(contact);
        }

        [HttpPost]
        public IActionResult DeleteCategory(int? id)
        {
            var contact = _unitOfWork.Contact.GetFirstOrDefault(x => x.Id == id);

            if (contact == null)
            {
                NotFound();
            }

            _unitOfWork.Contact.Remove(contact);
            _unitOfWork.Complete();
            TempData["Delete"] = "Data Has Deleted Successfully";
            return RedirectToAction("Index");
        }


    }
}
