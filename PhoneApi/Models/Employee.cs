using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Models
{
    public class Employee
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public ICollection<EmployeeCabinetPhone> EmployeeCabinetPhones { get; set; }
        [NotMapped]
        public ICollection<CabinetPhone> CabinetPhones { get; set; }

        public Employee()
        {
            EmployeeCabinetPhones = new List<EmployeeCabinetPhone>();
            CabinetPhones = new List<CabinetPhone>();
        }
    }
}
