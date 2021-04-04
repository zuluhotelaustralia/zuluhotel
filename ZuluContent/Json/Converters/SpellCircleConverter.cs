using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server.Spells;

namespace Server.Json
{
    public class SpellCircleConverter : JsonConverter<SpellCircle>
    {
        private readonly IEnumerable<SpellCircle> m_Circles;

        public SpellCircleConverter(IEnumerable<SpellCircle> circles)
        {
            m_Circles = circles;
        }
        
        public override SpellCircle Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                {
                    var name = reader.GetString();
                    return m_Circles.FirstOrDefault(v => v.Name.InsensitiveEquals(name));
                }
                case JsonTokenType.Number:
                {
                    var id = reader.GetInt32();
                    return m_Circles.FirstOrDefault(v => v.Id == id);
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