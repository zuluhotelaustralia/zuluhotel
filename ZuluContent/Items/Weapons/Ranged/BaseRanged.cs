using System;
using Server.Network;

namespace Server.Items
{
    public abstract class BaseRanged : BaseMeleeWeapon
    {
        public virtual int EffectId { get; set;  }
        public abstract Type AmmoType { get; }
        public abstract Item Ammo { get; }

        public override int DefaultHitSound => 0x234;

        public override int DefaultMissSound => 0x238;

        public override SkillName DefaultSkill => SkillName.Archery;

        public override WeaponType DefaultWeaponType => WeaponType.Ranged;

        public override WeaponAnimation DefaultAnimation => WeaponAnimation.ShootXBow;

        public override SkillName AccuracySkill => SkillName.Archery;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Balanced { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Velocity { get; set; }

        public BaseRanged(int itemId) : base(itemId)
        {
        }

        public BaseRanged(Serial serial) : base(serial)
        {
        }

        public override TimeSpan OnSwing(Mobile attacker, Mobile defender)
        {
            if (Core.TickCount > attacker.LastMoveTime + 1000)
            {
                if (attacker.HarmfulCheck(defender))
                {
                    attacker.DisruptiveAction();
                    attacker.NetState?.SendSwing(attacker.Serial, defender.Serial);

                    if (OnFired(attacker, defender))
                    {
                        if (CheckHit(attacker, defender))
                            OnHit(attacker, defender);
                        else
                            OnMiss(attacker, defender);
                    }
                }

                attacker.RevealingAction();

                return GetDelay(attacker);
            }
            else
            {
                attacker.RevealingAction();

                return TimeSpan.FromSeconds(0.25);
            }
        }

        public override void OnHit(Mobile attacker, Mobile defender, double damageBonus)
        {
            if (attacker.Player && !defender.Player && (defender.Body.IsAnimal || defender.Body.IsMonster) &&
                0.4 >= Utility.RandomDouble())
                defender.AddToBackpack(Ammo);

            base.OnHit(attacker, defender, damageBonus);
        }

        public override void OnMiss(Mobile attacker, Mobile defender)
        {
            if (attacker.Player && 0.4 >= Utility.RandomDouble())
            {
                Ammo.MoveToWorld(
                    new Point3D(defender.X + Utility.RandomMinMax(-1, 1), defender.Y + Utility.RandomMinMax(-1, 1),
                        defender.Z), defender.Map);
            }

            base.OnMiss(attacker, defender);
        }

        public virtual bool OnFired(Mobile attacker, Mobile defender)
        {
            if (attacker.Player)
            {
                Container pack = attacker.Backpack;

                if (pack == null || !pack.ConsumeTotal(AmmoType, 1))
                    return false;
            }

            attacker.MovingEffect(defender, EffectId, 18, 1, false, false);

            return true;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 3); // version

            writer.Write((bool) Balanced);
            writer.Write((int) Velocity);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 3:
                {
                    Balanced = reader.ReadBool();
                    Velocity = reader.ReadInt();

                    goto case 2;
                }
                case 2:
                case 1:
                {
                    break;
                }
                case 0:
                {
                    /*m_EffectID =*/
                    reader.ReadInt();
                    break;
                }
            }
        }
    }
}
