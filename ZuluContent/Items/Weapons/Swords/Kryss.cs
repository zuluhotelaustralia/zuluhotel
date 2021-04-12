namespace Server.Items
{
    [FlipableAttribute(0x1401, 0x1400)]
    public class Kryss : BaseSword
    {
        public override int DefaultMinDamage => 5;
        public override int DefaultMaxDamage => 11;
        public override int DefaultSpeed => 65;
        public override int InitMinHits => 90;
        public override int InitMaxHits => 90;
        public override SkillName DefaultSkill => SkillName.Fencing;
        public override WeaponType DefaultWeaponType => WeaponType.Piercing;
        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Pierce1H;

        [Constructible]
        public Kryss() : base(0x1401)
        {
            Weight = 2.0;
        }

        [Constructible]
        public Kryss(Serial serial) : base(serial)
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

            if (Weight == 1.0)
                Weight = 2.0;
        }
    }
}