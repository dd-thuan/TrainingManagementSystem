using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.ViewModel
{
    public class CourseUserViewModel
    {
        public ApplicationUser User { get; set; }
        public TrainerCourse TrainerUser { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}