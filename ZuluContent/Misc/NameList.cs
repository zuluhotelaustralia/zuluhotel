using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Server.Json;
using Server.Mobiles;

namespace Server
{
    public class NameList
    {
        public const string RandomNamePlaceholder = "<random>";

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("names")]
        public string[] List { get; set; }

        public bool ContainsName(string name)
        {
            for (int i = 0; i < List.Length; i++)
                if (name == List[i])
                    return true;

            return false;
        }

        public string GetRandomName() => List.Length > 0 ? List[Utility.Random(List.Length)] : "";

        public static NameList GetNameList(string type)
        {
            Table.TryGetValue(type.ToLowerInvariant(), out var n);
            return n;
        }

        public static string RandomName(string entry)
        {
            if (Table.TryGetValue(entry.ToLowerInvariant(), out var list))
                return GetNameList(entry)?.GetRandomName();

            try
            {
                list = Table.First(kv => entry.Contains(kv.Key, StringComparison.InvariantCultureIgnoreCase)).Value;
            }
            catch (InvalidOperationException)
            {
                list = GetNameList("male");
            }


            return list?.GetRandomName() ?? string.Empty;
        }


        private static readonly Dictionary<string, NameList> Table = new(StringComparer.OrdinalIgnoreCase);

        public static void Configure()
        {
            // TODO: Turn this into a command so it can be updated in-game
            string filePath = Path.Combine(Core.BaseDirectory, "Data/names.json");

            List<NameList> nameLists = JsonConfig.Deserialize<List<NameList>>(filePath);
            foreach (var nameList in nameLists)
            {
                nameList.FixNames();
                Table.Add(nameList.Type, nameList);
            }
        }

        public static bool HasPlaceholder(string name)
        {
            return name != null && name.Contains(RandomNamePlaceholder, StringComparison.InvariantCulture);
        }
        
        public static string SubstitutePlaceholderName(string name, string substitute)
        {
            if (!HasPlaceholder(name))
                return name;
            
            return name.Replace(RandomNamePlaceholder, substitute, StringComparison.InvariantCultureIgnoreCase);
        }

        private void FixNames()
        {
            for (int i = 0; i < List.Length; i++)
                List[i] = Utility.Intern(List[i].Trim());
        }
    }
}