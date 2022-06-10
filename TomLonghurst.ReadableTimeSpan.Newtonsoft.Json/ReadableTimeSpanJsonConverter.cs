using Newtonsoft.Json;

namespace TomLonghurst.ReadableTimeSpan.Newtonsoft.Json;

public class ReadableTimeSpanJsonConverter : JsonConverter<TimeSpan>
{
    public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
    {
        writer.WriteValue(value.ToString());
    }

    public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var value = reader.Value as string;

        if (string.IsNullOrEmpty(value))
        {
            return TimeSpan.Zero;
        }
        
        return ReadableTimeSpan.Parse(value);
    }
}