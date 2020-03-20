using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Models
{
    public class EmployeePhone
    {
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public long PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}
