using System.IO;
using Scripts.Zulu.Engines.Classes;
using Server.Items;
using Server.Json;
using Server.Mobiles;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments;

namespace Server.Commands
{
    public class TestHooks
    {
        public static void Initialize()
        {
            CommandSystem.Register("TestHooks", AccessLevel.Player, TestHooks_OnCommand);
            CommandSystem.Register("TestSerialization", AccessLevel.Player, TestSerialization_OnCommand);
        }

        [Usage("TestHooks")]
        private static void TestHooks_OnCommand(CommandEventArgs eventArgs)
        {
            var subject = new Rat();

            subject.Enchantments.Set((HealingBonus e) => e.Value = 25);
            subject.ZuluClass.Type = ZuluClassType.Warrior;
            subject.ZuluClass.Level = 5;
            var value = 100.0;

            eventArgs.Mobile.SendMessage($"Heal value before {value}");

            ((Mobile) subject).FireHook(h => h.OnHeal(subject, subject, new Bandage(), ref value));

            eventArgs.Mobile.SendMessage($"Healed value after {value}");

            subject.Delete();
        }

        [Usage("TestSerialization")]
        private static void TestSerialization_OnCommand(CommandEventArgs eventArgs)
        {
            var path = Path.Combine(Core.BaseDirectory, "test.json");
            var jsOpts = JsonConfig.GetOptions(new TextDefinitionConverterFactory(), new EnchantmentConverterFactory());

            JsonConfig.Serialize(path, new IEnchantmentValue[]
            {
                new AirProtection {Value = 100},
                new DurabilityBonus {Value = 10},
                new HealingBonus {Value = 25}
            }, jsOpts);

            var config = JsonConfig.Deserialize<IEnchantmentValue[]>(path, jsOpts);

            config.ToString();
        }
    }
}