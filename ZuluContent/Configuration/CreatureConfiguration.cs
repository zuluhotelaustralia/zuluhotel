using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
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
            Console.Write(
                $"\tDeserializeJsonConfig<CreatureConfiguration>: loading creatures from {BasePath}*.json ... ");

            ConvertTemplatesTest();

            var failed = new ConcurrentBag<string>();

            var files = Directory.GetFiles(BasePath, "*.json", new EnumerationOptions {RecurseSubdirectories = true});
            Parallel.ForEach(files, file =>
            {
                try
                {
                    var props = JsonConfig.Deserialize<CreatureProperties>(file, SerializerOptions);
                    if (props == null)
                    {
                        failed.Add($"Failed to load entry: {Path.GetFileName(file)}, serialization returned null");
                        return;
                    }

                    Entries.TryAdd(Path.GetFileNameWithoutExtension(file), props);
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
        
        private static void ConvertTemplatesTest()
        {
            Parallel.ForEach(CreatureProperties.Creatures, kv =>
            {
                var (type, props) = kv;
                var filePath = Path.Combine(BasePath, $"{type.Name}.json");
                JsonConfig.Serialize(filePath, props, SerializerOptions);
            });
        }

        static CreatureConfiguration()
        {
            CommandSystem.Register("ConvertTemplates", AccessLevel.Owner, ConvertTemplates_OnCommand);
        }

        [Usage("ConvertTemplates")]
        private static async void ConvertTemplates_OnCommand(CommandEventArgs e)
        {
            if (!(e.Mobile is PlayerMobile pm))
                return;

            foreach (var (type, props) in CreatureProperties.Creatures)
            {
                var creature = type.CreateInstance<BaseCreature>();

                if (creature == null)
                    return;

                props.Attack = new CreatureAttack
                {
                    Damage = (props.DamageMin ?? 0, props.DamageMax ?? props.DamageMin ?? 0),
                    Ability = props.WeaponAbility,
                    AbilityChance = props.WeaponAbilityChance,
                    HasBreath = props.HasBreath,
                    HasWebs = props.HasWebs
                };

                props.WeaponAbility = null;
                props.WeaponAbilityChance = null;
                props.HasBreath = null;
                props.HasWebs = null;
                props.DamageMin = null;
                props.DamageMax = null;

                foreach (var item in creature.Items)
                {
                    if (item is BaseWeapon weapon)
                    {
                        props.Attack.Speed = Math.Abs(weapon.DefaultSpeed - weapon.Speed) > 0.01 ? weapon.Speed : null;
                        props.Attack.Skill = weapon.DefaultSkill != weapon.Skill ? weapon.Skill : null;
                        props.Attack.Animation = weapon.DefaultAnimation != weapon.Animation ? weapon.Animation : null;
                        props.Attack.HitSound = weapon.HitSound;
                        props.Attack.MissSound = weapon.MissSound;
                        props.Attack.MaxRange = weapon.DefaultMaxRange != weapon.MaxRange ? weapon.MaxRange : null;
                    }

                    if (item is SkinningKnife)
                        continue;


                    var equip = new CreatureEquip
                    {
                        ItemType = item.GetType(),
                        Name = item.DefaultName != item.Name ? item.Name : null,
                        Hue = item.Hue,
                    };

                    if (item is BaseArmor armor)
                        equip.ArmorRating = armor.BaseArmorRating;

                    props.Equipment ??= new List<CreatureEquip>();
                    props.Equipment.Add(equip);
                }

                creature.Delete();

                var filePath = Path.Combine(BasePath, $"{type.Name}.json");
                JsonConfig.Serialize(filePath, props, SerializerOptions);
            }
        }
    }
}