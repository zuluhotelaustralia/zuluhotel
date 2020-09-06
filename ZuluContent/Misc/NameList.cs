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
            m_Table.TryGetValue(type, out NameList n);
            return n;
        }

        public static string RandomName(string type) => GetNameList(type)?.GetRandomName() ?? "";

        private static readonly Dictionary<string, NameList> m_Table = new Dictionary<string, NameList>(StringComparer.OrdinalIgnoreCase);

        public static void Configure()
        {
            // TODO: Turn this into a command so it can be updated in-game
            string filePath = Path.Combine(Core.BaseDirectory, "Data/names.json");

            List<NameList> nameLists = JsonConfig.Deserialize<List<NameList>>(filePath);
            foreach (var nameList in nameLists)
            {
                nameList.FixNames();
                m_Table.Add(nameList.Type, nameList);
            }
        }
        
        public static void SubstituteCreatureName(BaseCreature c)
        {
            if (!c.Name.Contains(RandomNamePlaceholder) || !c.CorpseNameOverride.Contains(RandomNamePlaceholder))
                return;

            string value;
            try
            {
                var pair = m_Table.First(
                    kv => c.Name.Contains(kv.Key, StringComparison.InvariantCultureIgnoreCase) ||
                          c.GetType().Name.Contains(kv.Key, StringComparison.InvariantCultureIgnoreCase)
                );
                value = pair.Value.GetRandomName();
            }
            catch (InvalidOperationException)
            {
                value = RandomName("male");
            }

            c.Name = c.Name.Replace(RandomNamePlaceholder, value, StringComparison.InvariantCultureIgnoreCase);

            c.CorpseNameOverride = c.CorpseNameOverride.Replace(RandomNamePlaceholder, value,
                StringComparison.InvariantCultureIgnoreCase);
        }

        private void FixNames()
        {
            for (int i = 0; i < List.Length; i++)
                List[i] = Utility.Intern(List[i].Trim());
        }
    }
}