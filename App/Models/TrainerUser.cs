using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class TrainerUser
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }
 
        [DisplayName("Type")]
        public GetType type { get; set; }
        public enum GetType
        {
            [Display(Name = "Selecte Type")]
            Selectetype = 0,
            Internal = 1,
            External = 2
        }
        [Required]
        [DisplayName("Phone Number")]

        public string Telephone { get; set; }
        [DisplayName("Working Place")]
        public string WorkingPlace { get; set; }
        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }
        [ForeignKey("Id")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}