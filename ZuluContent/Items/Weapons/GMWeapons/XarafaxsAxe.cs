using Server.Engines.Harvest;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Items
{
    public class XarafaxsAxe : BaseAxe, IUsesRemaining, IEnchanted
    {
        public override HarvestSystem HarvestSystem => Lumberjacking.System;

        public override int DefaultStrengthReq => 20;

        public override int DefaultMinDamage => 1;

        public override int DefaultMaxDamage => 15;

        public override int DefaultSpeed => 40;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Bash1H;


        [Constructible]
        public XarafaxsAxe() : this(200)
        {
        }


        [Constructible]
        public XarafaxsAxe(int uses) : base(0x0F49)
        {
            Name = "Xarafax's Axe";
            Weight = 11.0;
            Hue = 1162;
            UsesRemaining = uses;
            ShowUsesRemaining = true;
            HarvestBonus = 2;
            Identified = false;
        }

        [Constructible]
        public XarafaxsAxe(Serial serial) : base(serial)
        {
        }

        public override void OnSingleClick(Mobile from)
        {
            if (!string.IsNullOrEmpty(Name))
                LabelTo(from, Name);
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