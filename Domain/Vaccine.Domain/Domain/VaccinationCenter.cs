using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.Domain.Domain
{
    public class VaccinationCenter : BaseEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int MaxCapacity { get; set; }
        public virtual ICollection<Vaccines>? Vaccines { get; set; }
    }

}
