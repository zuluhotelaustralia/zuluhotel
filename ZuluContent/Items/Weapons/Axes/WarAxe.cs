using Server.Engines.Harvest;

namespace Server.Items
{
    [FlipableAttribute(0x13B0, 0x13AF)]
    public class WarAxe : BaseAxe
    {
        public override int DefaultHitSound => 0x23C;
        public override int DefaultMissSound => 0x232;
        public override int DefaultStrengthReq => 35;
        public override int DefaultMinDamage => 5;
        public override int DefaultMaxDamage => 21;
        public override int DefaultSpeed => 35;
        public override SkillName DefaultSkill => SkillName.Macing;
        public override WeaponType DefaultWeaponType => WeaponType.Bashing;
        public override int InitMinHits => 80;
        public override int InitMaxHits => 80;
        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Bash1H;

        [Constructible]
        public WarAxe() : base(0x13B0)
        {
            Weight = 8.0;
        }

        [Constructible]
        public WarAxe(Serial serial) : base(serial)
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