using System;
using Server;

namespace Server.Items
{
    public class GreaterConflagrationPotion : BaseConflagrationPotion
    {
        public override int MinDamage { get { return 4; } }
        public override int MaxDamage { get { return 8; } }

        [Constructable]
        public GreaterConflagrationPotion() : base(PotionEffect.ConflagrationGreater)
        {
        }

        public GreaterConflagrationPotion(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
