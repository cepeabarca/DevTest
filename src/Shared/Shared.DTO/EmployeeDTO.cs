using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class EmployeeDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Por favor indica tu nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Por favor indica tu apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El RFC es obligatorio")]
        [RFC(ErrorMessage = "Invalid RFC format")]
        public string RFC { get; set; }

        [Required(ErrorMessage = "Por favor indica tu fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BornDate { get; set; }

        [Required(ErrorMessage = "El Estado es obligatorio")]
        public EmployeeStatus Status { get; set; }
    }


}
