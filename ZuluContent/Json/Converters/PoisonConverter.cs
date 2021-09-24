using Server.Mobiles;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Server.Json
{
    public class PoisonConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(Poison);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            new PoisonConverter();
    }
    
    public class PoisonConverter : JsonConverter<Poison>
    {
        public override Poison Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            if(reader.TokenType != JsonTokenType.String)
                throw new JsonException();

            return Poison.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Poison value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}