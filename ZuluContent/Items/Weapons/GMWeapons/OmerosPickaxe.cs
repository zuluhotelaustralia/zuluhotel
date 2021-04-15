using Server.Engines.Harvest;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Items;

namespace Server.Items
{
    public class OmerosPickaxe : BaseAxe, IUsesRemaining, IGMItem
    {
        public override HarvestSystem HarvestSystem => Mining.System;

        public override int DefaultStrengthReq => 20;

        public override int DefaultMinDamage => 1;

        public override int DefaultMaxDamage => 15;

        public override int DefaultSpeed => 40;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Slash1H;


        [Constructible]
        public OmerosPickaxe() : this(200)
        {
        }


        [Constructible]
        public OmerosPickaxe(int uses) : base(0xE86)
        {
            Name = "Omero's Pickaxe";
            Weight = 11.0;
            Hue = 1301;
            UsesRemaining = uses;
            ShowUsesRemaining = true;
            HarvestBonus = 2;
            Identified = false;
        }

        [Constructible]
        public OmerosPickaxe(Serial serial) : base(serial)
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