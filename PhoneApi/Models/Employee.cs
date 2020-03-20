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
        public ICollection<EmployeePhone> EmployeePhones { get; set; }

        public Employee()
        {
            EmployeePhones = new List<EmployeePhone>();
        }
    }
}
