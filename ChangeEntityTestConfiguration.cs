using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestEFCore;
public class ChangeEntityTestConfiguration<TEntity, TKey, TUserKey> : IEntityTypeConfiguration<ChangeEntityTest<TEntity, TKey, TUserKey>>
    where TEntity : class, IEntityTest<TKey>, new()
{
    void IEntityTypeConfiguration<ChangeEntityTest<TEntity, TKey, TUserKey>>.Configure(EntityTypeBuilder<ChangeEntityTest<TEntity, TKey, TUserKey>> builder)
    {
        builder.ToTable(typeof(TEntity).Name + "_ChangeTracker");
        builder.HasOne(o => o.ChangeEntityInfoTest)
               .WithOne()
               .HasForeignKey<ChangeEntityInfoTest<TEntity, TKey, TUserKey>>(o => o.Id);
        // builder.OwnsOne(e => e.Object, builder =>
        // {
        //     builder.ToJson("Changes");
        // });
        //builder.Property(e => e.ChangeEntityInfoTest).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
    }
}