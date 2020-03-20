using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneApi.Models
{
    public class Cabinet
    {
        public long Id { get; set; }
        public string CabinetNumber { get; set; }
         public ICollection<CabinetPhone> CabinetPhones { get; set; }

        public Cabinet()
        {
            CabinetPhones = new List<CabinetPhone>();
        }
    }
}
