using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.ViewModel
{
    public class TraineeUserCourseViewModel
    {
        public ApplicationUser User { get; set; }
        public TraineeCourse TraineeUser { get; set; }
        public IEnumerable<Course> Courses { get; set; }

    }
}