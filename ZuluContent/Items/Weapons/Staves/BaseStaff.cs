namespace Server.Items
{
    public abstract class BaseStaff : BaseMeleeWeapon
    {
        public override int DefaultHitSound => 0x13B;
        public override int DefaultMissSound => 0x233;

        public override SkillName DefaultSkill => SkillName.Macing;

        public override WeaponType DefaultWeaponType => WeaponType.Staff;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Slash2H;

        public BaseStaff(int itemID) : base(itemID)
        {
        }

        public BaseStaff(Serial serial) : base(serial)
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

        public override bool AllowEquippedCast(Mobile from)
        {
            return true;
        }
    }
}