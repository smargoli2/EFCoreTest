using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestEFCore;
public class ChangeEntityInfoPersonConfiguration : IEntityTypeConfiguration<ChangeEntityInfoPerson>
{
    void IEntityTypeConfiguration<ChangeEntityInfoPerson>.Configure(EntityTypeBuilder<ChangeEntityInfoPerson> builder)
    {
        builder.ToTable(typeof(Person).Name + "_ChangeTracker");
        builder.Property(e => e.Changes).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(e => e.Changes).HasJsonConversion();
    }
}