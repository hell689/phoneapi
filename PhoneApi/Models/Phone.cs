using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Models
{
    public class Phone
    {
        public long Id { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<CabinetPhone> CabinetPhones { get; set; }
        [NotMapped]
        public ICollection<Cabinet> Cabinets { get; set; }

        public Phone()
        {
            CabinetPhones = new List<CabinetPhone>();
            Cabinets = new List<Cabinet>();
        }
    }
}
