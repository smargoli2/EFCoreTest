using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace TestEFCore;
public class ChangeEntityTest<TEntity, TKey, TUserKey> : EntityTest<TKey>
    where TEntity : class, IEntityTest<TKey>, new()
{
    public State State { get; set; }
    public TUserKey UserId { get; set; }
    public DateTime Date { get; set; }
    public TKey ObjectId { get; set; }

    [ForeignKey(nameof(ObjectId))]
    public TEntity Object { get; set; }

    public ChangeEntityInfoTest<TEntity, TKey, TUserKey> ChangeEntityInfoTest { get; set; }
    public TEntity GetChangedEntity()
    {
        var result = ChangeEntityInfoTest?.ChangedEntity;
        if (result == null)
        {
            throw new Exception("ChangeEntityInfo is not loaded.  Use ChangeEntityRepo to load explicitly");
        }
        return result;
    }
}
public class ChangeEntityInfoTest<TEntity, TKey, TUserKey> : EntityTest<TKey>
    where TEntity : class, IEntityTest<TKey>, new()
{
    private readonly JsonObjectWrapper<TEntity> _changesWrapper;

    public ChangeEntityInfoTest()
    {
        _changesWrapper = new JsonObjectWrapper<TEntity>(false);
    }

    [NotMapped]
    public JObject Changes
    {
        get => _changesWrapper.JObject;
        set => _changesWrapper.JObject = value;
    }

    [NotMapped]
    public virtual TEntity ChangedEntity
    {
        get => _changesWrapper.Value;
        set => _changesWrapper.Value = value;
    }
}

public enum State : int
{
    Created = 1,
    Modified = 2,
    Deleted = 3,
}