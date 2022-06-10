using System.Text.Json;
using System.Text.Json.Serialization;

namespace TomLonghurst.ReadableTimeSpan.System.Text.Json;

public class ReadableTimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();

        if (string.IsNullOrEmpty(value))
        {
            return TimeSpan.Zero;
        }
        
        return ReadableTimeSpan.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}