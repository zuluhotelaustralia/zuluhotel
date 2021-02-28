using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using MessagePack;
using MessagePack.Resolvers;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Json
{
    /// <summary>
    /// Read and Writes IEnchantmentValue objects to JSON
    /// </summary>
    /// <remarks>This implementation is very unoptimized and only to be used only for small configuration loading</remarks>

    public class EnchantmentConverter : JsonConverter<IEnchantmentValue>
    {
        private static readonly MessagePackSerializerOptions MessagePackOptions = ContractlessStandardResolver
            .Options
            .WithResolver(DynamicContractlessObjectResolver.Instance);

        public override IEnchantmentValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();
            if (reader.GetString() != "Type") throw new JsonException();
            
            reader.Read();
            if (reader.TokenType != JsonTokenType.String) throw new JsonException();
            var typeName = reader.GetString();
            
            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();
            if (reader.GetString() != "Properties") throw new JsonException();

            reader.Read();
            if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

            var rawText = JsonDocument.ParseValue(ref reader).RootElement.GetRawText();
            
            var obj = MessagePackSerializer.Deserialize(
                AssemblyHandler.FindTypeByName(typeName), 
                MessagePackSerializer.ConvertFromJson(rawText, MessagePackOptions), 
                MessagePackOptions
            );

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return obj as IEnchantmentValue;
                }
            }
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IEnchantmentValue value, JsonSerializerOptions options)
        {
            var method = GetType().GetMethod(nameof(Serialize))!.MakeGenericMethod(value.GetType());
            var json = (string)method.Invoke(null, new object[] {value});

            using (var document = JsonDocument.Parse(json))
            {
                writer.WriteStartObject();
                writer.WritePropertyName("Type");
                writer.WriteStringValue(value.GetType().Name);
                writer.WritePropertyName("Properties");
                document.RootElement.WriteTo(writer);
                writer.WriteEndObject();
            }
        }

        public static string Serialize<T>(T value)
        {
            return MessagePackSerializer.ConvertToJson(MessagePackSerializer.Serialize(value, MessagePackOptions));
        }
    }
}
