using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class TraineeCourse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TraineeId { get; set; }
        [ForeignKey("TraineeId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string TraineeName { get; set; }
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string CourseName { get; set; }
    }
}