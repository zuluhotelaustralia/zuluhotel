using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Server.Scripts.Engines.Loot;

namespace Server.Json
{
    public class LootTableConverterFactory : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(LootTable);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            new LootTableConverter();
    }
    
    public class LootTableConverter : JsonConverter<LootTable>
    {
        public override LootTable Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            var table = new LootTable();

            if (reader.TokenType != JsonTokenType.StartArray)
                throw new JsonException();
            
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                ReadEntry(table, ref reader);
            }

            return table;
        }

        private void ReadEntry(LootGroup table, ref Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartObject) 
                throw new JsonException();
            
            var minQuantity = 1;
            var maxQuantity = 1;
            var chance = 0.0;
            Type type = null;
            LootGroup group = null;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType != JsonTokenType.PropertyName) 
                    throw new JsonException();

                var property = reader.GetString();
                reader.Read();

                switch (property)
                {
                    case "MinQuantity" when reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out minQuantity):
                        break;
                    case "MaxQuantity" when reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out maxQuantity):
                        break;
                    case "Chance" when reader.TokenType == JsonTokenType.Number && reader.TryGetDouble(out chance):
                        break;
                    case "Value" when reader.TokenType == JsonTokenType.String && reader.GetString() is { } typeName:
                        type = AssemblyHandler.FindTypeByName(typeName);
                        break;
                    case "Value" when reader.TokenType == JsonTokenType.StartArray:
                        group = new LootGroup();
                        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
                        {
                            ReadEntry(group, ref reader);
                        }
                        break;
                }
            }

            if (group != null && table is LootTable tbl)
            {
                tbl.Add(group, minQuantity, maxQuantity, chance);
            }
            else if (type != null)
            {
                table.Add(type, minQuantity, maxQuantity, chance);
            }
        }

        public override void Write(Utf8JsonWriter writer, LootTable value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}