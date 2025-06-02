using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectHelper.Domain.Users
{
    public class SkillsDictionaryJsonConverter : JsonConverter<Dictionary<string, string>>
    {
        public override Dictionary<string, string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var dictionary = new Dictionary<string, string>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException();
                }

                string key = reader.GetString();
                reader.Read();
                string value = reader.GetString();

                dictionary.Add(key, value);
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, string> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (var kvp in value)
            {
                writer.WritePropertyName(kvp.Key);
                writer.WriteStringValue(kvp.Value);
            }

            writer.WriteEndObject();
        }
    }
} 