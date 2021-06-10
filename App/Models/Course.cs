using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [DisplayName("Course Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Description")]
        public string Description { get; set; }
    }
}