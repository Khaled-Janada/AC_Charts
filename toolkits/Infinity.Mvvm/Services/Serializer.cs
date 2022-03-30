using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using Infinity.ComponentModel.ViewModels;
using Infinity.Messaging.Messages;

namespace Infinity.Services;

public class Serializer : ViewModelBase {

    public JsonSerializerOptions JsonSerializerOptions { get; }

    public Serializer() {
        JsonSerializerOptions = new JsonSerializerOptions();
    }

    public Serializer(JsonSerializerOptions serializerOptions) {
        JsonSerializerOptions = serializerOptions;
    }

    public void Save<T>(T obj, string filePath) {
        var jsonString = JsonSerializer.Serialize(obj, JsonSerializerOptions);
        File.WriteAllText(filePath, jsonString);
    }

    public void SaveWithDialog<T>(T obj, string objName, string objType, string ext) where T : class {
        var path = Request<SaveRequestMessage, string?>(new SaveRequestMessage(objName, objType, ext));
        if (path is null) return;

        Save(obj, path);
    }

    public T Load<T>(string filePath) {
        var jsonString = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(jsonString, JsonSerializerOptions) ?? throw new SerializationException();
    }

    public T? LoadWithDialog<T>(string objType, string ext) where T : class {
        var path = Request<LoadRequestMessage, string?>(new LoadRequestMessage(objType, ext));

        return path is null ? null : Load<T>(path);
    }

}
