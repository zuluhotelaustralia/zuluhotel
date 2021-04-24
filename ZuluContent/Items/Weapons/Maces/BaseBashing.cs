namespace Server.Items
{
    public abstract class BaseBashing : BaseMeleeWeapon
    {
        public override int DefaultHitSound => 0x13B;
        public override int DefaultMissSound => 0x233;

        public override SkillName DefaultSkill => SkillName.Macing;

        public override WeaponType DefaultWeaponType => WeaponType.Bashing;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.Bash1H;

        public BaseBashing(int itemID) : base(itemID)
        {
        }

        public BaseBashing(Serial serial) : base(serial)
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

        public override void OnHit(Mobile attacker, Mobile defender)
        {
            base.OnHit(attacker, defender);

            defender.Stam -= Utility.RandomMinMax(1, 3); // 1-3 points of stamina loss
        }
    }
}