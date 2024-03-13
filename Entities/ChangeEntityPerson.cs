using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace TestEFCore;
public class ChangeEntityPerson : EntityTest<int>
{
    public State State { get; set; }
    public string UserId { get; set; }
    public DateTime Date { get; set; }
    public ChangeEntityInfoPerson ChangeEntityInfoPerson { get; set; }
    public Person GetChangedEntity()
    {
        var result = ChangeEntityInfoPerson?.ChangedEntity;
        if (result == null)
        {
            throw new Exception("ChangeEntityInfo is not loaded.  Use ChangeEntityRepo to load explicitly");
        }
        return result;
    }
}
public class ChangeEntityInfoPerson : EntityTest<int>
{
    private readonly JsonObjectWrapper<Person> _changesWrapper;

    public ChangeEntityInfoPerson()
    {
        _changesWrapper = new JsonObjectWrapper<Person>(false);
    }

    [NotMapped]
    public JObject Changes
    {
        get => _changesWrapper.JObject;
        set => _changesWrapper.JObject = value;
    }

    [NotMapped]
    public virtual Person ChangedEntity
    {
        get => _changesWrapper.Value;
        set => _changesWrapper.Value = value;
    }
}