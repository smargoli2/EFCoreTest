using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestEFCore;
public class JsonObjectWrapper<T> where T : class, new()
{
    private readonly bool _isNullable;
    private JObject _jobject;
    private T _value;

    private static readonly JsonSerializer _serializer = JsonSerializer.Create(new JsonObjectWrapperSerializerSettings());
    private static readonly JsonMergeSettings _mergeSettings = new JsonMergeSettings()
    {
        MergeArrayHandling = MergeArrayHandling.Replace,
        MergeNullValueHandling = MergeNullValueHandling.Merge
    };

    public JsonObjectWrapper(bool isNullable)
    {
        _isNullable = isNullable;
    }

    public JObject JObject
    {
        get
        {
            if (_isNullable && _jobject == null && _value == null)
            {
                return null;
            }

            var valueAsJobject = JObject.FromObject(Value, _serializer);

            if (_jobject != null)
            {
                _jobject.Merge(valueAsJobject, _mergeSettings);
            }
            else
            {
                _jobject = valueAsJobject;
            }

            return _jobject;
        }

        set
        {
            _jobject = value;
            if (_jobject != null)
            {
                _value = _jobject.ToObject<T>(_serializer);
            }
            else if (_isNullable)
            {
                _value = null;
            }
        }
    }

    public T Value
    {
        get
        {
            if (_isNullable && _value == null)
            {
                return null;
            }

            return _value ?? (_value = new T());
        }
        set
        {
            _value = value;

            if (_isNullable && value == null)
            {
                _jobject = null;
            }
        }
    }
}