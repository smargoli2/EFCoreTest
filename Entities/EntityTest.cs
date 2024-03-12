using System.ComponentModel.DataAnnotations;

namespace TestEFCore;
public abstract class EntityTest<T> : IEntityTest<T>, IHasId<T>
{

    [Key]
    public virtual T Id { get; set; }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public override string ToString()
    {
        return $"{GetType().Name}(Id:{Id})";
    }
}

public abstract class EntityTest : EntityTest<int>, IHasIntId { }

public interface IEntityTest<T>
{
    T Id { get; set; }
}

public interface IHasId<T>
{
    T Id { get; }
}

public interface IHasIntId : IHasId<int>
{
    string GetHumanReadableId()
        => throw new NotImplementedException();
}

public interface IChangeTrackerEntity<TEntity, TKey, TUserKey>
        where TEntity : class, IEntityTest<TKey>, new()
{
    ICollection<ChangeEntityTest<TEntity, TKey, TUserKey>> Changes { get; }
}