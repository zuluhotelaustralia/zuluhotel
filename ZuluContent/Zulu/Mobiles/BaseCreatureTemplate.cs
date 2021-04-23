using System.IO;
using Server.Json;

namespace Server.Mobiles
{
    public class BaseCreatureTemplate : BaseCreature
    {
        public static void Initialize()
        {
            CommandSystem.Register("SerializeTemplates", AccessLevel.Owner, SerializeTemplates_OnCommand);
        }

        [Usage("SerializeTemplates")]
        [Description("Serializes and overwrites all templates on disk")]
        private static async void SerializeTemplates_OnCommand(CommandEventArgs eventArgs)
        {
            var path = Path.Combine(Core.BaseDirectory, "Data/Creatures/");
            var jsOpts = JsonConfig.GetOptions(
                new TextDefinitionConverterFactory(),
                new CreaturePropConverterFactory(),
                new BodyConverterFactory(),
                new PoisonConverterFactory(),
                new WeaponAbilityConverterFactory()
            );
            
            foreach (var (type, props) in CreatureProperties.Creatures)
            {
                var filePath = Path.Combine(path, $"{type.Name}.json");
                JsonConfig.Serialize(filePath, props, jsOpts);
            
                
                // TODO: remove testing code
                var d = JsonConfig.Deserialize<CreatureProperties>(filePath, jsOpts);
            }
        }

        protected BaseCreatureTemplate(CreatureProperties p) : base(p)
        {
        }

        public BaseCreatureTemplate(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(1);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}