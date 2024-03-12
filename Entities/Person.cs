using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace TestEFCore;
public class Person : EntityTest, IChangeTrackerEntity<Person, int, string>
{
    public string Name { get; set; }
    public DateTime? ConfirmationDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<ChangeEntityTest<Person, int, string>> Changes { get; private set; } = new HashSet<ChangeEntityTest<Person, int, string>>();
}