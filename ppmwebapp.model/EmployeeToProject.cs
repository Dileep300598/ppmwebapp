using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ppmwebapp.model
{
    public class EmployeeToProject
    {
        [Key]
        public int ProjectId { get; set; }

        public string EmployeeName { get; set; }
       
    }
}
