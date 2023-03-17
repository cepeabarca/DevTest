using Shared.DTO;
using System.ComponentModel.DataAnnotations;

namespace DevTest.BackEnd.Data.Models
{
    public class Employee
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string RFC { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BornDate { get; set; }

        public EmployeeStatus Status { get; set; }
    }

}