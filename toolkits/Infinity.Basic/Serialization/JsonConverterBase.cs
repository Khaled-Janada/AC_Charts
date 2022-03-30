using System.Runtime.Serialization;

namespace Infinity.Serialization;

public abstract class JsonConverterBase<T1, T2> : JsonConverter<T1> {

    protected abstract T1 Read(T2 modelData);
    protected abstract T2 Write(T1 model);

    public override T1 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        var modelData = JsonSerializer.Deserialize<T2>(ref reader);

        return Read(modelData ?? throw new SerializationException());
    }

    public override void Write(Utf8JsonWriter writer, T1 value, JsonSerializerOptions options) {
        var modelData = Write(value);

        JsonSerializer.Serialize(writer, modelData);
    }

}