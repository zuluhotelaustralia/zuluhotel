using Server.Engines.Harvest;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Items
{
    public class OmerosPickaxe : BaseAxe, IUsesRemaining, IEnchanted
    {
        public override HarvestSystem HarvestSystem
        {
            get => Mining.System;
        }

        public override int DefaultStrengthReq
        {
            get => 20;
        }

        public override int DefaultMinDamage
        {
            get => 1;
        }

        public override int DefaultMaxDamage
        {
            get => 15;
        }

        public override int DefaultSpeed
        {
            get => 40;
        }

        public override WeaponAnimation DefaultAnimation
        {
            get => WeaponAnimation.Slash1H;
        }


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
        }

        [Constructible]
        public OmerosPickaxe(Serial serial) : base(serial)
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