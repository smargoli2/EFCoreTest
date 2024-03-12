
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestEFCore;
public static class DbSetJsonConversionExtension
{
    private static JTokenEqualityComparer _jTokenEqualityComparer = new JTokenEqualityComparer();

    private static ValueComparer<JObject> _jsonComparer = new ValueComparer<JObject>(
        (j1, j2) => _jTokenEqualityComparer.Equals(j1, j2),
        j => _jTokenEqualityComparer.GetHashCode(j),
        j => j == null ? null : JObject.Parse(j.ToString())
    );

    private static ValueConverter<JObject, string> _jsonConverter = new ValueConverter<JObject, string>(
        (jo) => jo.ToString(Formatting.None),
        (s) => JObject.Parse(s)
    );

    public static PropertyBuilder<JObject> HasJsonConversion(this PropertyBuilder<JObject> builder)
    {
        builder.HasConversion(_jsonConverter);
        builder.Metadata.SetValueComparer(_jsonComparer);
        return builder;
    }
}