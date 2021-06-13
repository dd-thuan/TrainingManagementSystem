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
        private readonly ApplicationDbContext _context;
      
        public TrainerController()
        {
            _context = new ApplicationDbContext();
         
        }


        // GET: Trainer
        public ActionResult ViewMyProfile()
        {
            var CurrentTrainerId = User.Identity.GetUserId();
            var trainerInDb = _context.trainerUsers.SingleOrDefault(t => t.Id == CurrentTrainerId);
            return View(trainerInDb);
        }



        public ActionResult AssignedCourse()
        {
            var CurrentTrainerId = User.Identity.GetUserId();
            var trainerCourse = _context.trainerCourses.Where(t => t.TrainerId == CurrentTrainerId).Include(c => c.Course.Category).ToList();
            return View(trainerCourse);
        }


        //
        //Edit
        [HttpGet]
        public ActionResult EditProfile()
        {
            var CurrentUserId = User.Identity.GetUserId();
            var UserInDb = _context.trainerUsers.SingleOrDefault(c => c.Id == CurrentUserId);
            return View(UserInDb);
        }
        [HttpPost]
        public ActionResult EditProfile(TrainerUser trainer)
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
            return RedirectToAction("ViewMyProfile", "Trainer");
        }
    }
}