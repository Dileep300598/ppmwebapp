using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppmwebapp.model
{
    public class Project
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Please Enter ProjectName")]
        [StringLength(100), MinLength(2)]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please Enter StartDate")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Please Enter StartDate")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Please Enter StartDate")]
        public decimal Budget { get; set; }
    }
}
