using Server.Mobiles;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server.Utilities;
using ZuluContent.Zulu.Skills;

namespace Server.Json
{
    public class SkillHandlerConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(BaseSkillHandler);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            new SkillHandlerConverter();
    }
    
    public class SkillHandlerConverter : JsonConverter<BaseSkillHandler>
    {
        public override BaseSkillHandler Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            
            if(reader.TokenType != JsonTokenType.String)
                throw new JsonException();

            var typeName = reader.GetString();
            var type = AssemblyHandler.FindTypeByName(typeName);
            
            if (!type.IsAssignableTo(typeof(BaseSkillHandler)))
            {
                throw new ArgumentOutOfRangeException(
                    $"Skill handler of type {type} must inherit from {typeof(BaseSkillHandler)}");
            }
            
            var handler = type.CreateInstance<BaseSkillHandler>();
            if (handler == null)
            {
                throw new ArgumentNullException(
                    $"Unable to create {nameof(BaseSkillHandler)} of type {type}");
            }

            return handler;
        }

        public override void Write(Utf8JsonWriter writer, BaseSkillHandler value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.GetType().Name);
        }
    }
}