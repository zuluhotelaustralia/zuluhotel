using Server.Mobiles;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server.Engines.Magic.HitScripts;
using Server.Utilities;

namespace Server.Json
{
    public class WeaponAbilityConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(WeaponAbility);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            new WeaponAbilityConverter();
    }
    
    public class WeaponAbilityConverter : JsonConverter<WeaponAbility>
    {
        public override WeaponAbility Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.StartObject:
                {
                    reader.Read();
                    
                    if (reader.TokenType != JsonTokenType.PropertyName) 
                        throw new JsonException();
                    if (reader.GetString()?.ToLowerInvariant() != nameof(Type).ToLowerInvariant()) 
                        throw new JsonException();

                    reader.Read();
                    if (reader.TokenType != JsonTokenType.String) 
                        throw new JsonException();

                    WeaponAbility ability = null;
                    switch (reader.GetString())
                    {
                        case nameof(SpellStrike):
                        {
                            reader.Read();
                            if (reader.TokenType != JsonTokenType.PropertyName) 
                                throw new JsonException();
                            if (reader.GetString()?.ToLowerInvariant() != nameof(SpellStrike.SpellType).ToLowerInvariant()) 
                                throw new JsonException();

                            reader.Read();
                            if (reader.TokenType != JsonTokenType.String) 
                                throw new JsonException();

                            var type = AssemblyHandler.FindTypeByName(reader.GetString());
                            
                            ability = new SpellStrike(type);
                            break;
                        }
                    }

                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonTokenType.EndObject)
                            return ability;
                    }

                    throw new JsonException();
                }
                case JsonTokenType.String:
                {
                    var name = reader.GetString();
                    var type = AssemblyHandler.FindTypeByName(reader.GetString());

                    if (type.IsSubclassOf(typeof(WeaponAbility)))
                        return type.CreateInstance<WeaponAbility>();

                    throw new JsonException($"WeaponAbility {name} did not match a known weapon ability");
                }
            }
            
            throw new JsonException("WeaponAbility property did not match a known weapon ability");
        }

        public override void Write(Utf8JsonWriter writer, WeaponAbility value, JsonSerializerOptions options)
        {
            switch(value)
            {
                case SpellStrike spellStrike:
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName(nameof(Type));
                    writer.WriteStringValue(value.GetType().Name);
                    writer.WritePropertyName(nameof(SpellStrike.SpellType));
                    writer.WriteStringValue(spellStrike.SpellType.Name);
                    writer.WriteEndObject();
                    break;
                }
                default:
                    writer.WriteStringValue(value.GetType().Name);
                    break;
            };
        }
    }
}