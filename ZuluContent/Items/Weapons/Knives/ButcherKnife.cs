namespace Server.Items
{
    [FlipableAttribute(0x13F6, 0x13F7)]
    public class ButcherKnife : BaseKnife
    {
        public override int DefaultMinDamage => 2;
        public override int DefaultMaxDamage => 8;
        public override SkillName DefaultSkill => SkillName.Swords;
        public override int DefaultSpeed => 45;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public ButcherKnife() : base(0x13F6)
        {
            Weight = 1.0;
        }

        [Constructible]
        public ButcherKnife(Serial serial) : base(serial)
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