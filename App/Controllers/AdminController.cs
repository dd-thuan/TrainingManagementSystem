using App.Models;
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

       
    }
}