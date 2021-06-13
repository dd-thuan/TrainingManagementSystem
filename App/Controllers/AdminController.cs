using App.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public AdminController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }



        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StaffList()
        {
            var roleId = _context.Roles.Where(r => r.Name.Equals("Staff")).FirstOrDefault().Id;
            var staffInDb = _context.Users.Where(s => s.Roles.Any(r => r.RoleId == roleId));
            return View(staffInDb);
        }

        [HttpGet]
        public ActionResult CreateStaff()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStaff(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = _userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRole(user.Id, "Staff");
                    _context.SaveChanges();
                }
                return RedirectToAction("StaffList");
            }
            return View(model);
        }

        public ActionResult DeleteStaff(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(s => s.Id == id);
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            return RedirectToAction("StaffList");
        }







        [HttpGet]
        public ActionResult CreateTrainer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainer(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = _userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRole(user.Id, "Trainer");
                    var trainerUser = new TrainerUser()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        FullName = model.FullName,
                        Telephone = model.Telephone,
                        WorkingPlace = model.WorkingPlace,
                        type = model.Type,
                        EmailAddress = user.UserName
                    };
                    _context.trainerUsers.Add(trainerUser);
                }
                _context.SaveChanges();
                return RedirectToAction("TrainerList");
            }
            return View(model);
        }
        public ActionResult TrainerList(string searchString)
        {
            var trainerInDb = _context.trainerUsers.ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                trainerInDb = _context.trainerUsers
                .Where(m => m.FullName.Contains(searchString) || m.Telephone.Contains(searchString))
                .ToList();
            }

            return View(trainerInDb);
        }

        public ActionResult TrainerProfile(string id)
        {
            var trainerInDb = _context.trainerUsers.SingleOrDefault(t => t.Id == id);

            return View(trainerInDb);
        }

        [HttpGet]
        public ActionResult UpdateProfileTrainer(string id)
        {
            var trainerInDd = _context.trainerUsers.SingleOrDefault(c => c.Id == id);
            return View(trainerInDd);
        }

        [HttpPost]
        public ActionResult UpdateProfileTrainer(TrainerUser trainer)
        {
            var trainerInDb = _context.trainerUsers.SingleOrDefault(t => t.Id == trainer.Id);
            {
                trainerInDb.WorkingPlace = trainer.WorkingPlace;
                trainerInDb.type = trainer.type;
                trainerInDb.Telephone = trainer.Telephone;
                trainerInDb.EmailAddress = trainer.EmailAddress;
            }
            _context.SaveChanges();
            return RedirectToAction("TrainerList");
        }

        public ActionResult DeleteTrainer(string id)
        {
            var userInDb = _context.Users.SingleOrDefault(s => s.Id == id);
            var trainerInDb = _context.trainerUsers.SingleOrDefault(t => t.Id == id);
            _context.trainerUsers.Remove(trainerInDb);
            _context.Users.Remove(userInDb);
            _context.SaveChanges();
            return RedirectToAction("TrainerList");
        }


    }
}