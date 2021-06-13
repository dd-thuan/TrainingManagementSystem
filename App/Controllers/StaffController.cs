using App.Models;
using App.ViewModel;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class StaffController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public StaffController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
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
            var course = _context.courses.Include(t => t.Category).ToList();
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
        //TraineeList_Create-DeleteAccount_UpdateProfile_AssignCourse_

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


        public ActionResult ViewCourseAssignedTrainee(string id)
        {
            var traineeCourse = _context.traineeCourses.Where(t => t.TraineeId == id).ToList();
            return View(traineeCourse);
        }

        [HttpGet]
        public ActionResult AssignCourseTrainee(string id)
        {
            var UserInDb = _context.Users.SingleOrDefault(t => t.Id == id);
            var viewModel = new TraineeUserCourseViewModel()
            {
                User = UserInDb,
                Courses = _context.courses.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AssignCourseTrainee(TraineeUserCourseViewModel traineeCourse)
        {
            var newTraineeCourse = new TraineeCourse()
            {
                TraineeId = traineeCourse.User.Id,
                CourseId = traineeCourse.TraineeUser.CourseId,
            };
            var TraineeCourseInDb = _context.traineeCourses.Add(newTraineeCourse);
            var traineeUserObject = _context.traineeUsers.SingleOrDefault(t => t.Id == TraineeCourseInDb.TraineeId);
            var CourseObject = _context.courses.SingleOrDefault(t => t.Id == TraineeCourseInDb.CourseId);
            TraineeCourseInDb.TraineeName = traineeUserObject.UserName;
            TraineeCourseInDb.CourseName = CourseObject.Name;
            _context.SaveChanges();
            return RedirectToAction("TraineeList");
        }

        [HttpGet]
        public ActionResult ChangeCourseTrainee(int id)
        {
            var traineeCourseInDb = _context.traineeCourses.SingleOrDefault(t => t.Id == id);
            var viewModel = new TraineeUserCourseViewModel()
            {
                TraineeUser = traineeCourseInDb,
                Courses = _context.courses.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ChangeCourseTrainee(TraineeUserCourseViewModel traineeCourse)
        {
            var traineeCourseInDb = _context.traineeCourses.SingleOrDefault(t => t.Id == traineeCourse.TraineeUser.Id);
            var courseInDb = _context.courses.SingleOrDefault(t => t.Id == traineeCourse.TraineeUser.CourseId);
            traineeCourseInDb.CourseId = traineeCourse.TraineeUser.CourseId;
            traineeCourseInDb.CourseName = courseInDb.Name;
            _context.SaveChanges();
            return RedirectToAction("TraineeList");
        }

        public ActionResult RemoveCourseTrainee(int id)
        {
            var traineeCourseInDb = _context.traineeCourses.SingleOrDefault(c => c.Id == id);
            _context.traineeCourses.Remove(traineeCourseInDb);
            _context.SaveChanges();
            return RedirectToAction("ViewCourseAssignedTrainee");
        }






        //
        //TrainerList_CreateAccount_Assign-Change-DeleteCourseTrainer
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







      

        //
        //
        //
        public ActionResult ViewCourseAssignedTrainer(string id)
        {
            var trainerCourse = _context.trainerCourses.Where(t => t.TrainerId == id).ToList();
            return View(trainerCourse);
        }

        [HttpGet]
        public ActionResult AssignCourseTrainer(string id)
        {
            var UserInDb = _context.Users.SingleOrDefault(t => t.Id == id);
            var viewModel = new CourseUserViewModel()
            {
                User = UserInDb,
                Courses = _context.courses.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AssignCourseTrainer(CourseUserViewModel trainerCourse)
        {

            var newTrainerCourse = new TrainerCourse()
            {
                TrainerId = trainerCourse.User.Id,
                CourseId = trainerCourse.TrainerUser.CourseId,
            };
            var TrainerCourseInDb = _context.trainerCourses.Add(newTrainerCourse);
            var trainerUserObject = _context.trainerUsers.SingleOrDefault(t => t.Id == TrainerCourseInDb.TrainerId);
            var CourseObject = _context.courses.SingleOrDefault(t => t.Id == TrainerCourseInDb.CourseId);
            TrainerCourseInDb.TrainerName = trainerUserObject.UserName;
            TrainerCourseInDb.CourseName = CourseObject.Name;
            _context.SaveChanges();
            return RedirectToAction("TrainerList");
        }

        [HttpGet]
        public ActionResult ChangeCourseTrainer(int id)
        {
            var trainerCourseInDb = _context.trainerCourses.SingleOrDefault(t => t.Id == id);
            var viewModel = new CourseUserViewModel()
            {
                TrainerUser = trainerCourseInDb,
                Courses = _context.courses.ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ChangeCourseTrainer(CourseUserViewModel trainerCourse)
        {
            var trainerCourseInDb = _context.trainerCourses.SingleOrDefault(t => t.Id == trainerCourse.TrainerUser.Id);
            var courseInDb = _context.courses.SingleOrDefault(t => t.Id == trainerCourse.TrainerUser.CourseId);
            trainerCourseInDb.CourseId = trainerCourse.TrainerUser.CourseId;
            trainerCourseInDb.CourseName = courseInDb.Name;
            _context.SaveChanges();
            return RedirectToAction("TrainerList");
        }


        public ActionResult RemoveCourseTrainer(int id)
        {
            var trainerCourseInDb = _context.trainerCourses.SingleOrDefault(c => c.Id == id);
            _context.trainerCourses.Remove(trainerCourseInDb);
            _context.SaveChanges();
            return RedirectToAction("ViewCourseAssignedTrainer");
        }
    }       
}