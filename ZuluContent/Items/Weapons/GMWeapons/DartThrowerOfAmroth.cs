using System;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    [FlipableAttribute(0x13FD, 0x13FC)]
    public class DartThrowerOfAmroth : BaseRanged, IGMItem
    {
        public override int EffectId => 0x379F;
        public override int DefaultHitSound => 0x206;

        public override Type AmmoType => typeof(ThunderBolt);

        public override Item Ammo => new ThunderBolt();

        public override int DefaultStrengthReq => 80;

        public override int DefaultMinDamage => 30;

        public override int DefaultMaxDamage => 65;

        public override int DefaultSpeed => 35;

        public override int DefaultMaxRange => 7;

        public override int InitMinHits => 70;

        public override int InitMaxHits => 70;

        [Constructible]
        public DartThrowerOfAmroth() : base(0x13FD)
        {
            Name = "One Handed Dart Thrower of Amroth";
            Hue = 1181;
            Identified = false;
            Weight = 9.0;
            Layer = Layer.OneHanded;
        }

        [Constructible]
        public DartThrowerOfAmroth(Serial serial) : base(serial)
        {
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 0); // version
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}