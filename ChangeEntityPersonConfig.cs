using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestEFCore;
public class ChangeEntityPersonConfiguration : IEntityTypeConfiguration<ChangeEntityPerson>
{
    void IEntityTypeConfiguration<ChangeEntityPerson>.Configure(EntityTypeBuilder<ChangeEntityPerson> builder)
    {
        builder.ToTable(typeof(Person).Name + "_ChangeTracker");
        builder.HasOne(o => o.ChangeEntityInfoPerson)
               .WithOne()
               .HasForeignKey<ChangeEntityInfoPerson>(o => o.Id);
    }
}