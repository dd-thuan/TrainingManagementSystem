using App.Models;
using App.ViewModel;
using Microsoft.Ajax.Utilities;
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
    public class StaffController : Controller
    {

        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public StaffController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(
               new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }




        //
        //
        //Course_List_And_CRUD
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
            return RedirectToAction("CourseList");
        }

        public ActionResult DeleteCourse(int id)
        {
            var courseInDb = _context.courses.SingleOrDefault(c => c.Id == id);
            _context.courses.Remove(courseInDb);
            _context.SaveChanges();
            return RedirectToAction("CourseList");
        }




        //
        //
        //Category_List_And_CRUD
        public ActionResult CategoryList(string searchString)
        {
            var categories = _context.categories.ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                categories = _context.categories
                .Where(c => c.Name.Contains(searchString))
                .ToList();
            }
            return View(categories);
        }

        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                var newCategories = new Category()
                {
                    Name = category.Name,
                    Description = category.Description
                };
                _context.categories.Add(newCategories);
                _context.SaveChanges();
                return RedirectToAction("CategoryList");
            }
            return View();
        }

        public ActionResult CategoryDetails(int id)
        {
            var categoryInDb = _context.categories.SingleOrDefault(c => c.Id == id);

            return View(categoryInDb);
        }

        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var categoryInDb = _context.categories.SingleOrDefault(c => c.Id == id);
            return View(categoryInDb);
        }
        [HttpPost]
        public ActionResult EditCategory(Category category)
        {
            var categoryInDb = _context.categories.SingleOrDefault(c => c.Id == category.Id);
            categoryInDb.Name = category.Name;
            categoryInDb.Description = category.Description;
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        public ActionResult DeleteCategory(int id)
        {
            var categoryInDb = _context.categories.SingleOrDefault(c => c.Id == id);
            _context.categories.Remove(categoryInDb);
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }





        //
        //
        //List_Trainee_CRUD

        public ActionResult TraineeList(string searchString)
        {
            var traineeInDb = _context.traineeUsers.ToList();
            if (!searchString.IsNullOrWhiteSpace())
            {
                traineeInDb = _context.traineeUsers
                .Where(m => m.FullName.Contains(searchString) || m.Telephone.Contains(searchString))
                .ToList();
            }
            return View(traineeInDb);
        }

        public ActionResult CreateTrainee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTrainee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var today = DateTime.Today;
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = _userManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    _userManager.AddToRole(user.Id, "Trainee");
                    var traineeUser = new TraineeUser()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        FullName = model.FullName,
                        DateOfBirth = model.DateOfBirth,
                        age = today.Year - model.DateOfBirth.Year,
                        Telephone = model.Telephone,
                        mainProgrammingLanguage = model.mainProgrammingLangueage,
                        ToeicScore = model.ToeicSocre,
                        Department = model.Department,
                        EmailAddress = user.UserName
                    };
                    _context.traineeUsers.Add(traineeUser);
                    _context.SaveChanges();
                    return RedirectToAction("TraineeList");
                }

            }
            return View(model);
        }

        public ActionResult TraineeProfile(string id)
        {
            var traineeInDb = _context.traineeUsers.SingleOrDefault(t => t.Id == id);
            return View(traineeInDb);
        }

        [HttpGet]
        public ActionResult UpdateProfileTrainee(string id)
        {
            var traineeInDb = _context.traineeUsers.SingleOrDefault(t => t.Id == id);
            return View(traineeInDb);
        }
        [HttpPost]
        public ActionResult UpdateProfileTrainee(TraineeUser trainee)
        {
            var traineeInDb = _context.traineeUsers.SingleOrDefault(t => t.Id == trainee.Id);
            {
                traineeInDb.UserName = trainee.EmailAddress;
                traineeInDb.FullName = trainee.FullName;
                traineeInDb.DateOfBirth = trainee.DateOfBirth;
                traineeInDb.age = trainee.age;
                traineeInDb.Department = trainee.Department;
                traineeInDb.Telephone = trainee.Telephone;
                traineeInDb.ToeicScore = trainee.ToeicScore;
                traineeInDb.mainProgrammingLanguage = trainee.mainProgrammingLanguage;
                traineeInDb.EmailAddress = trainee.EmailAddress;
            }
            var userInDb = _context.Users.SingleOrDefault(u => u.Id == trainee.Id);
            {
                userInDb.UserName = trainee.EmailAddress;
            }
            _context.SaveChanges();
            return RedirectToAction("TraineeList");
        }

    }
}