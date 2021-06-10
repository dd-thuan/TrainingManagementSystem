using App.Models;
using App.ViewModel;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class StaffController : Controller
    {

        private ApplicationDbContext _context;

        public StaffController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseList(string searchString)
        {
            var course = _context.courses.Include(t =>t.Category).ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                course = _context.courses
                .Where(c => c.Name.Contains(searchString) || c.Category.Name.Contains(searchString))
                .Include(c => c.Category)
                .ToList();
            }
            return View(course);
        }

        [HttpGet]
        public ActionResult CreateCourse()
        {
            var viewModel = new CourseCategoryViewModel()
            {
                Categories = _context.categories.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CreateCourse(CourseCategoryViewModel course)
        {
            if (ModelState.IsValid)
            {
                var newCourse = new Course()
                {
                    Name = course.Courses.Name,
                    Description = course.Courses.Description,
                    CategoryId = course.Courses.CategoryId
                };
                _context.courses.Add(newCourse);
                _context.SaveChanges();
                return RedirectToAction("CourseList");
            }
            return View(course);
        }

        public ActionResult CourseDetails(int id)
        {
            var courseInDb = _context.courses.SingleOrDefault(c => c.Id == id);
            var viewModel = new CourseCategoryViewModel()
            {
                Courses = courseInDb,
                Categories = _context.categories.Where(c => c.Id == courseInDb.CategoryId).ToList()
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult EditCourse(int id)
        {
            var courseInDb = _context.courses.SingleOrDefault(c => c.Id == id);
            var viewModel = new CourseCategoryViewModel()
            {
                Courses = courseInDb,
                Categories = _context.categories.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult EditCourse(CourseCategoryViewModel course)
        {
            var courseInDb = _context.courses.SingleOrDefault(c => c.Id == course.Courses.Id);
            courseInDb.Name = course.Courses.Name;
            courseInDb.Description = course.Courses.Description;
            courseInDb.CategoryId = course.Courses.CategoryId;
            _context.SaveChanges();
            return RedirectToAction("ListCourse");
        }

        public ActionResult DeleteCourse(int id)
        {
            var courseInDb = _context.courses.SingleOrDefault(c => c.Id == id);
            _context.courses.Remove(courseInDb);
            _context.SaveChanges();
            return RedirectToAction("ListCourse");
        }
    }
}