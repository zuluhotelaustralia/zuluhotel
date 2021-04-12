namespace Server.Items
{
    [FlipableAttribute(0xEC3, 0xEC2)]
    public class Cleaver : BaseKnife
    {
        public override int DefaultMinDamage => 3;
        public override int DefaultMaxDamage => 9;
        public override SkillName DefaultSkill => SkillName.Swords;
        public override int DefaultSpeed => 40;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public Cleaver() : base(0xEC3)
        {
            Weight = 2.0;
        }

        [Constructible]
        public Cleaver(Serial serial) : base(serial)
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