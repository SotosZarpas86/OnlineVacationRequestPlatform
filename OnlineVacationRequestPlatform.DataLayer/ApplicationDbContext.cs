using Microsoft.EntityFrameworkCore;
using OnlineVacationRequestPlatform.DataLayer.Entities;

namespace OnlineVacationRequestPlatform.DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<VacationRequest> VacationRequests { get; set; }
    }
}
