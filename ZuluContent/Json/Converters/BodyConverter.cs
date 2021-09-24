using Server.Mobiles;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Server.Json
{
    public class BodyConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(Body);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            new BodyConverter();
    }
    
    public class BodyConverter : JsonConverter<Body>
    {
        public override Body Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            
            if(reader.TokenType != JsonTokenType.Number)
                throw new JsonException();

            return reader.GetInt32();
        }

        public override void Write(Utf8JsonWriter writer, Body value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }
    }
}