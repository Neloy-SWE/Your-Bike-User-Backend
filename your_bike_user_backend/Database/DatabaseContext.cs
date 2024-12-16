using Microsoft.EntityFrameworkCore;
using your_bike_user_backend.Models;

namespace your_bike_user_backend.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<Bike> Bikes { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
