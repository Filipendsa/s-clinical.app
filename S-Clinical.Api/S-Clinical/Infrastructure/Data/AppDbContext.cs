using Microsoft.EntityFrameworkCore;

namespace S_Clinical.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Domain.Entities.Patient> Patients { get; set; }
        public DbSet<Domain.Entities.ClinicalCare> ClinicalCares { get; set; }
        public DbSet<Domain.Entities.Triage> Triages { get; set; }

    }
}
