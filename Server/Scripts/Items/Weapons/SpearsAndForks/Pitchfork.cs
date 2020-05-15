using System;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    [FlipableAttribute(0xE87, 0xE88)]
    public class Pitchfork : BaseSpear
    {
        public override WeaponAbility PrimaryAbility { get { return WeaponAbility.BleedAttack; } }
        public override WeaponAbility SecondaryAbility { get { return WeaponAbility.Dismount; } }

        public override int AosStrengthReq { get { return 55; } }
        public override int AosMinDamage { get { return 13; } }
        public override int AosMaxDamage { get { return 14; } }
        public override int AosSpeed { get { return 43; } }
        public override float MlSpeed { get { return 2.50f; } }

        public override int DefMaxRange { get { return 2; } }

        public override int OldStrengthReq { get { return 15; } }
        public override int OldMinDamage { get { return 4; } }
        public override int OldMaxDamage { get { return 16; } }
        public override int OldSpeed { get { return 45; } }

        public override int InitMinHits { get { return 31; } }
        public override int InitMaxHits { get { return 60; } }

        public override double GetBaseDamage(Mobile attacker)
        {
            if (attacker is BaseCreature)
            {
                return base.GetBaseDamage(attacker);
            }

            int damage = Utility.Dice(4, 4, 0);

            if (DamageLevel != WeaponDamageLevel.Regular)
            {
                damage += (2 * (int)DamageLevel) - 1;
            }

            return damage;
        }

        [Constructable]
        public Pitchfork() : base(0xE87)
        {
            Weight = 11.0;
        }

        public Pitchfork(Serial serial) : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (Weight == 10.0)
                Weight = 11.0;
        }
    }
}
