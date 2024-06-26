﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.Domain.Domain
{
    public class Patient : BaseEntity
    {
        public string? Embg { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public ICollection<Vaccines>? VaccinationSchedule { get; set; }
    }

}
