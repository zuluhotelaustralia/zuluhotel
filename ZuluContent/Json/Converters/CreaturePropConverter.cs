using Server.Mobiles;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Server.Json
{
    public class CreaturePropConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(CreatureProp);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            new CreaturePropConverter();
    }

    public class CreaturePropConverter : JsonConverter<CreatureProp>
    {
        public override CreatureProp Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            double? min = null;
            double? max = null;

            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                {
                    reader.Read();
                    while (reader.TokenType != JsonTokenType.EndObject)
                    {
                        if (reader.TokenType != JsonTokenType.PropertyName)
                            throw new JsonException();

                        var propertyName = reader.GetString()?.ToLowerInvariant();
                        reader.Read();
                        if (reader.TokenType != JsonTokenType.Number)
                            throw new JsonException();

                        if (propertyName == nameof(CreatureProp.Min).ToLowerInvariant())
                            min = reader.GetDouble();
                        else if (propertyName == nameof(CreatureProp.Max).ToLowerInvariant()) 
                            max = reader.GetDouble();

                        reader.Read();
                    }

                    if (!min.HasValue)
                        throw new JsonException();

                    return new CreatureProp(min.Value, max);
                }
                case JsonTokenType.Number:
                {
                    return reader.GetDouble();
                }
                default:
                    throw new JsonException("CreatureProp value must be an integer or an object of { Min, Max }");
            }
        }

        public override void Write(Utf8JsonWriter writer, CreatureProp value, JsonSerializerOptions options)
        {
            if (value.Max.HasValue)
            {
                writer.WriteStartObject();
                writer.WritePropertyName(nameof(CreatureProp.Min));
                writer.WriteNumberValue(value.Min);
                writer.WritePropertyName(nameof(CreatureProp.Max));
                writer.WriteNumberValue(value.Max.Value);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNumberValue(value.Min);
            }
        }
    }
}