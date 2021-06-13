using App.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class TraineeController : Controller
    {
        private ApplicationDbContext _context;

        public TraineeController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Trainee
        public ActionResult MyProfile()
        {
            var CurrentTraineeId = User.Identity.GetUserId();
            var TraineeInDb = _context.traineeUsers.SingleOrDefault(t => t.Id == CurrentTraineeId);
            return View(TraineeInDb);
        }

        public ActionResult AvailableCourse(string searchString)
        {
            var course = _context.courses.Include(c => c.Category).ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                course = _context.courses
                .Where(c => c.Name.Contains(searchString) || c.Category.Name.Contains(searchString))
                .Include(c => c.Category)
                .ToList();
            }
            return View(course);
        }

        public ActionResult AssignedCourse()
        {
            var CurrentTraineeId = User.Identity.GetUserId();
            var traineeCourse = _context.traineeCourses.Where(t => t.TraineeId == CurrentTraineeId).Include(c => c.Course.Category).ToList();
            return View(traineeCourse);
        }


    }
}