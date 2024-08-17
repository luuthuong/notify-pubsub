using System.ComponentModel;
using System.Text.Json.Serialization;

namespace VNG.SocialNotify;

public class JsonDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.Parse(reader.GetString());
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-dd-MMTHH:mm:ss"));
    }
}

public static class Extension
{
    public static T LoadJsonFileData<T>(string path)
    {
        var json = File.ReadAllText(path);
        var data = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions()
        {
            Converters = {
                new JsonStringEnumConverter(),
                new JsonDateTimeConverter()
            }
        })!;
        return data;
    }
}
