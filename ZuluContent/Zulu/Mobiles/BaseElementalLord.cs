using Server;
using Server.Mobiles;

namespace ZuluContent.Zulu.Mobiles
{
    public class BaseElementalLord : BaseCreatureTemplate
    {
        [Constructible]
        public BaseElementalLord(string templateName) : base(templateName)
        {
        }

        public BaseElementalLord(Serial serial) : base(serial)
        {
        }
        
        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write(0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);
            var version = reader.ReadInt();
        }
    }
}