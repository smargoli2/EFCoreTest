using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestEFCore;
public class ChangeEntityInfoTestConfiguration<TEntity, TKey, TUserKey> : IEntityTypeConfiguration<ChangeEntityInfoTest<TEntity, TKey, TUserKey>>
    where TEntity : class, IEntityTest<TKey>, new()
{
    void IEntityTypeConfiguration<ChangeEntityInfoTest<TEntity, TKey, TUserKey>>.Configure(EntityTypeBuilder<ChangeEntityInfoTest<TEntity, TKey, TUserKey>> builder)
    {
        builder.ToTable(typeof(TEntity).Name + "_ChangeTracker");
        builder.Property(e => e.Changes).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        builder.Property(e => e.Changes).HasJsonConversion();
    }
}