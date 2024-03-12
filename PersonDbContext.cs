using Microsoft.EntityFrameworkCore;

namespace TestEFCore;
public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
    {
    }

    public DbSet<Person> Person { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");
            });

        modelBuilder.ApplyConfiguration(new ChangeEntityTestConfiguration<Person, int, string>());
        modelBuilder.ApplyConfiguration(new ChangeEntityInfoTestConfiguration<Person, int, string>());
    }
}