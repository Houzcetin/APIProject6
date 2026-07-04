using APIProject6.WebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIProject6.WebAPI.Context
{
    public class APIContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;initial catalog = APIYummyDb6;integrated security = true;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<YummyEvent> YummyEvents { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<EmployeeTask> EmployeeTasks { get; set; }
        public DbSet<EmployeeTaskChef> EmployeeTaskChefs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeTaskChef>()
                .HasKey(x => new { x.EmployeeTaskId, x.ChefId });

            modelBuilder.Entity<EmployeeTaskChef>()
                .HasOne(x => x.EmployeeTask)
                .WithMany(x => x.EmployeeTaskChefs)
                .HasForeignKey(x => x.EmployeeTaskId);

            modelBuilder.Entity<EmployeeTaskChef>()
                .HasOne(x => x.Chef)
                .WithMany(x => x.EmployeeTaskChefs)
                .HasForeignKey(x => x.ChefId);
        }

    }
}
