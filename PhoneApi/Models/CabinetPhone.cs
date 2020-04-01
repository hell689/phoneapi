using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Models
{
    public class CabinetPhone
    {
        public long Id { get; set; }
        public long CabinetId { get; set; }
        public Cabinet Cabinet { get; set; }

        public long PhoneId { get; set; }
        public Phone Phone { get; set; }

        public ICollection<EmployeeCabinetPhone> EmployeeCabinetPhones { get; set; }
    }
}
