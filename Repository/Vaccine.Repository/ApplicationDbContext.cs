using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vaccine.Domain.Domain;

namespace Vaccine.Repository
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Vaccines> Vaccines { get; set; }
        public virtual DbSet<VaccinationCenter> VaccinationCenters { get; set; }

    }
}
