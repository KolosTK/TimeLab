    using Microsoft.EntityFrameworkCore;
    using TimeLab.Entities;

    namespace TimeLab.Data;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }