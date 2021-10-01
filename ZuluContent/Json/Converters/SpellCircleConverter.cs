using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server.Spells;

namespace Server.Json
{
    public class SpellCircleConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(SpellCircle);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            new SpellCircleConverter();
    }
    
    public class SpellCircleConverter : JsonConverter<SpellCircle>
    {
        public override SpellCircle Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                {
                    return reader.GetString();
                }
                case JsonTokenType.Number:
                {
                    return reader.GetInt32();
                }
                default:
                    throw new JsonException("SpellCircle value must be an integer or string");
            }
        }

        public override void Write(Utf8JsonWriter writer, SpellCircle value, JsonSerializerOptions options)
        {
            if (value.Name == null)
                writer.WriteStringValue(value.Name);
            else
                writer.WriteNumberValue(value.Id);
        }
    }
}