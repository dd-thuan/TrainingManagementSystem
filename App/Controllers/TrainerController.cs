using App.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class TrainerController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TrainerController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }


        // GET: Trainer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewProfile()
        {
            var CurrentTrainerId = User.Identity.GetUserId();
            var trainerInDb = _context.trainerUsers.SingleOrDefault(t => t.Id == CurrentTrainerId);
            return View(trainerInDb);
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            var CurrentUserId = User.Identity.GetUserId();
            var UserInDb = _context.trainerUsers.SingleOrDefault(c => c.Id == CurrentUserId);
            return View(UserInDb);
        }
        [HttpPost]
        public ActionResult UpdateProfile(TrainerUser trainer)
        {
            var CurrentUserId = User.Identity.GetUserId();
            var UserInDb = _context.Users.SingleOrDefault(c => c.Id == CurrentUserId);
            var TrainerInDb = _context.trainerUsers.SingleOrDefault(c => c.Id == CurrentUserId);
            UserInDb.Email = trainer.EmailAddress;
            UserInDb.UserName = trainer.EmailAddress;
            TrainerInDb.FullName = trainer.FullName;
            TrainerInDb.EmailAddress = trainer.EmailAddress;
            TrainerInDb.Telephone = trainer.Telephone;
            TrainerInDb.type = trainer.type;
            TrainerInDb.WorkingPlace = trainer.WorkingPlace;
            TrainerInDb.UserName = trainer.EmailAddress;
            _context.SaveChanges();
            return RedirectToAction("ViewProfile", "Trainers");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string password)
        {
            var CurrentTrainerId = User.Identity.GetUserId();
            var TrainerInDb = _userManager.FindById(CurrentTrainerId);
            string newPassword = password;
            _userManager.RemovePassword(CurrentTrainerId);
            _userManager.AddPassword(CurrentTrainerId, newPassword);
            _userManager.Update(TrainerInDb);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ViewAssignedCourse()
        {
            var CurrentTrainerId = User.Identity.GetUserId();
            var trainerCourse = _context.trainerCourses.Where(t => t.TrainerId == CurrentTrainerId).Include(c => c.Course.Category).ToList();
            return View(trainerCourse);
        }
    }
}