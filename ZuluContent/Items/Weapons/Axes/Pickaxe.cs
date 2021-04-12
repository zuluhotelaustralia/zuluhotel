using Server.Engines.Harvest;

namespace Server.Items
{
    [FlipableAttribute(0xE86, 0xE85)]
    public class Pickaxe : BaseAxe, IUsesRemaining
    {
        public override HarvestSystem HarvestSystem => Mining.System;

        public override int DefaultStrengthReq => 15;
        public override int DefaultMinDamage => 1;
        public override int DefaultMaxDamage => 15;
        public override int DefaultSpeed => 35;
        public override SkillName DefaultSkill => SkillName.Macing;
        public override WeaponType DefaultWeaponType => WeaponType.Bashing;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;
        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Bash2H;

        [Constructible]
        public Pickaxe() : base(0xE86)
        {
            Weight = 11.0;
            Layer = Layer.TwoHanded;
            UsesRemaining = 50;
            ShowUsesRemaining = true;
        }

        [Constructible]
        public Pickaxe(Serial serial) : base(serial)
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
            ShowUsesRemaining = true;
        }
    }
}