using Newtonsoft.Json;

namespace TestEFCore;
public class JsonObjectWrapperSerializerSettings : JsonSerializerSettings
{
    public JsonObjectWrapperSerializerSettings()
    {
        DefaultValueHandling = DefaultValueHandling.Include;
        Formatting = Formatting.None;
        NullValueHandling = NullValueHandling.Include;
        ObjectCreationHandling = ObjectCreationHandling.Replace;
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    }
}