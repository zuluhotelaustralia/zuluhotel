using System;
using System.IO;
using System.Linq;
using Server;
using Server.Items;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enums;

namespace Scripts.Zulu.Engines.Magic
{
    public static class Commands
    {
        public static void Initialize()
        {
            CommandSystem.Register("TestMagicMod", AccessLevel.Developer, TestMagicMod_OnCommand);
            CommandSystem.Register("TestConvertSpells", AccessLevel.Developer, TestConvertSpells_OnCommand);
        }

        public static void TestMagicMod_OnCommand(CommandEventArgs e)
        {
            for (int i = 0; i < 10_000; i++)
            {
                var item = new Robe();

                // item.MagicProps.AddMod(new MagicStatMod(StatType.Str, 15));
                // item.MagicProps.TryGetMod(StatType.Str, out IMagicMod<StatType> strMod);
                //
                // var x = new MagicAttribute<int>(MagicProp.WeaponDamage, 6);
                //
                // item.MagicProps.SetAttr(ArmorDurabilityLevel.Tempered);
                // item.MagicProps.SetAttr(MagicProp.Accuracy, 25);
                //
                // var durability = item.MagicProps.GetAttr<ArmorDurabilityLevel>();
                // item.MagicProps.GetAttr<int>(MagicProp.Accuracy);
                // item.MagicProps[MagicProp.WeaponDamage]?.ToString();
                // item.MagicProps.SetAttr<ArmorDurabilityLevel, int>(10);
                //
                //
                // foreach (SkillName en in Enum.GetValues(typeof(SkillName)))
                // {
                //     item.MagicProps.AddMod(new MagicSkillMod(en, (int)en));
                // }
                //
                // item.MagicProps.TryGetMod(SkillName.Anatomy, out IMagicMod<SkillName> anatMod);
            
                item.MoveToWorld(e.Mobile.Location, Map.Internal);
            }
        }

        public static void TestConvertSpells_OnCommand(CommandEventArgs e)
        {
            
        }
    }
}