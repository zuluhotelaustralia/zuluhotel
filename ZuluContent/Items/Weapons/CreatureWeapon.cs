using System;

namespace Server.Items
{
    public class CreatureWeapon : BaseMeleeWeapon
    {
        public override int DefaultStrengthReq { get; } = 0;

        public override int DefaultMinDamage { get; } = 1;

        public override int DefaultMaxDamage { get; } = 8;

        public override int DefaultSpeed { get; } = 30;

        public override int DefaultHitSound { get; } = -1;

        public override int DefaultMissSound { get; } = -1;

        public override SkillName DefaultSkill { get; } = SkillName.Wrestling;

        public override WeaponType DefaultWeaponType { get; } = WeaponType.Fists;

        public override WeaponAnimation DefaultAnimation { get; } = WeaponAnimation.Wrestle;

        public CreatureWeapon() : base(0xEC4)
        {
            Visible = false;
            Movable = false;
            Mark = MarkQuality.Regular;
        }

        public CreatureWeapon(Serial serial) : base(serial)
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