using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Models
{
    public class EmployeeCabinetPhone
    {
        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public long CabinetPhoneId { get; set; }
        public CabinetPhone CabinetPhone { get; set; }
    }
}
