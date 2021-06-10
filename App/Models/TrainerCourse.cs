using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class TrainerCourse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TrainerId { get; set; }
        [ForeignKey("TrainerId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string TrainerName { get; set; }
        [Required]
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string CourseName { get; set; }
    }
}