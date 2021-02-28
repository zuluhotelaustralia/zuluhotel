using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Json
{
    public class EnchantmentConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(IEnchantmentValue);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            new EnchantmentConverter();
    }
}