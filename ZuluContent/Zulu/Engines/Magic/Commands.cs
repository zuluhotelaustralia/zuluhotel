using System;
using System.IO;
using System.Linq;
using Scripts.Engines.Magic;
using Server;
using Server.Items;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Engines.Magic
{
    public static class Commands
    {
        public static void Initialize()
        {
            CommandSystem.Register("TestMagicMod", AccessLevel.Developer, TestMagicMod_OnCommand);
            CommandSystem.Register("TestAttributeMod", AccessLevel.Developer, TestAttributeMod_OnCommand);
        }

        public static void TestMagicMod_OnCommand(CommandEventArgs e)
        {
            var item = new Halberd();

            item.MagicProps.AddMod(new MagicStatMod(StatType.Str, 15));
            item.MagicProps.TryGetMod(StatType.Str, out IMagicMod<StatType> strMod);

            var x = new MagicAttribute<int>(MagicProp.Damage, 6);

            item.MagicProps.SetAttr(WeaponDurabilityLevel.Indestructible);
            item.MagicProps.SetAttr(MagicProp.Accuracy, 25);

            var durability = item.MagicProps.GetAttr<WeaponDurabilityLevel>();
            var accuracy = item.MagicProps.GetAttr<WeaponAccuracyLevel, int>();
            item.MagicProps.GetAttr<int>(MagicProp.Accuracy);
            item.MagicProps[MagicProp.Damage]?.ToString();
            item.MagicProps.SetAttr<WeaponDurabilityLevel, int>(10);


            foreach (SkillName en in Enum.GetValues(typeof(SkillName)))
            {
                item.MagicProps.AddMod(new MagicSkillMod(en, (int)en));
            }

            item.MagicProps.TryGetMod(SkillName.Anatomy, out IMagicMod<SkillName> anatMod);
            var names = anatMod.CursedNames;

            var file = new FileInfo("magicprops.bin");
            using var fsWrite = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var writer = new BinaryFileWriter(fsWrite, true);
            
            try
            {
                item.MagicProps.Serialize(writer);
            }
            finally
            {
                writer.Flush();
                writer.Close();
            }

            using var fsRead = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var reader = new BinaryFileReader(new BinaryReader(fsRead));
            try
            {
                var magicalProperties = MagicalProperties.Deserialize(reader);

                magicalProperties.ToString();
            }
            finally
            {
                reader.Close();
            }


            item.Map = e.Mobile.Map;
            item.MoveToWorld(e.Mobile.Location);
        }

        public static void TestAttributeMod_OnCommand(CommandEventArgs e)
        {
            var item = new Halberd();
            



        }
    }
}