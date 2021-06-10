using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.ViewModel
{
    public class CourseCategoryViewModel
    {
        public Course Courses { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}