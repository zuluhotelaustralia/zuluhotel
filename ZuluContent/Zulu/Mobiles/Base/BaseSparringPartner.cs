using Server;
using Server.Mobiles;

namespace Server.Mobiles
{
    public class BaseSparringPartner : BaseCreatureTemplate
    {
        public override bool DisallowAllMoves { get; set; } = true;

        [Constructible]
        public BaseSparringPartner(string templateName) : base(templateName)
        {
        }

        public BaseSparringPartner(Serial serial) : base(serial)
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