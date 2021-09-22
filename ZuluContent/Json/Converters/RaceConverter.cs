using Server.Mobiles;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Server.Json
{
    public class RaceConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(Race);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            new RaceConverter();
    }
    
    public class RaceConverter : JsonConverter<Race>
    {
        public override Race Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            
            if(reader.TokenType != JsonTokenType.String)
                throw new JsonException();

            return Race.Parse(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Race value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Name);
        }
    }
}