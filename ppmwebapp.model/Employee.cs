using System;
using System.ComponentModel.DataAnnotations;

namespace ppmwebapp.model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter FirstName")]
        [StringLength(100),MinLength(2)]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Please Enter LastName")]
        [StringLength(100), MinLength(2)]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Please Enter Phone number")]
        [Display(Name ="Contact")]
        [RegularExpression(@"^([0-9]{10})$",ErrorMessage ="Please Enter Valid Number.")]
        public long Contact { get; set; }
       
        public string Role { get; set; }
    }
}
