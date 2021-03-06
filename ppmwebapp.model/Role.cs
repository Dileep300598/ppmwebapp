using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppmwebapp.model
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter RoleName")]
        public string RoleName { get; set; }
    }
}
