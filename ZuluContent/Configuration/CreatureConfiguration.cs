using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Server;
using Server.Items;
using Server.Json;
using Server.Mobiles;
using Server.Mobiles.Monsters;
using Server.Targeting;
using Server.Utilities;

namespace ZuluContent.Configuration
{
    public class CreatureConfiguration : BaseSingleton<CreatureConfiguration>
    {
        private static readonly string BasePath = Path.Combine(Core.BaseDirectory, "Data/Creatures/");
        private static readonly string TemplatePath = Path.Combine(BasePath, "Templates/");


        private static readonly JsonSerializerOptions SerializerOptions = JsonConfig.GetOptions(
            new TextDefinitionConverterFactory(),
            new CreaturePropConverterFactory(),
            new BodyConverterFactory(),
            new PoisonConverterFactory(),
            new WeaponAbilityConverterFactory()
        );

        public readonly ConcurrentDictionary<string, CreatureProperties> Entries =
            new(StringComparer.InvariantCultureIgnoreCase);

        protected CreatureConfiguration()
        {
            LoadCreatures();
        }

        private void LoadCreatures()
        {
            Console.Write(
                $"\tDeserializeJsonConfig<CreatureConfiguration>: loading templates from {TemplatePath}*.json ... ");


            var failed = new ConcurrentBag<string>();

            var files = Directory.GetFiles(TemplatePath, "*.json",
                new EnumerationOptions {RecurseSubdirectories = true});
            Parallel.ForEach(files, file =>
            {
                try
                {
                    var key = Path.GetFileNameWithoutExtension(file);
                    var props = JsonConfig.Deserialize<CreatureProperties>(file, SerializerOptions);
                    if (props == null)
                    {
                        failed.Add($"Failed to load entry: {key}, serialization returned null");
                        return;
                    }

                    if (Entries.ContainsKey(key))
                        Entries.Remove(key, out _);

                    Convert(file, props);

                    Entries.TryAdd(key, props);
                }
                catch (Exception e)
                {
                    failed.Add($"\tEncountered {e.GetType().Name} when loading {Path.GetFileName(file)}: {e.Message}");
                }
            });

            Console.Write(
                $"{Entries.Count} entries loaded{(!failed.IsEmpty ? $", {failed.Count} failed" : null)} ... ");
            Utility.PushColor(ConsoleColor.Green);
            Console.WriteLine("done.");
            Utility.PopColor();

            if (!failed.IsEmpty)
            {
                Utility.PushColor(ConsoleColor.Yellow);
                foreach (var reason in failed)
                    Console.WriteLine($"\t\t{reason}");
                Utility.PopColor();
            }
        }

        static CreatureConfiguration()
        {
            CommandSystem.Register("LoadCreatureTemplates", AccessLevel.Owner, _ => Instance.LoadCreatures());
        }
        
        private static void Convert(string file, CreatureProperties props)
        {
            var key = Path.GetFileNameWithoutExtension(file);

            if (props.WeaponAbility != null)
            {
                props.Attack.Ability = props.WeaponAbility;
                if (props.WeaponAbilityChance != null)
                {
                    props.Attack.AbilityChance = props.WeaponAbilityChance;
                }

                props.WeaponAbility = null;
                props.WeaponAbilityChance = null;
            }
            
            JsonConfig.Serialize(file, props, SerializerOptions);
        }
    }
}