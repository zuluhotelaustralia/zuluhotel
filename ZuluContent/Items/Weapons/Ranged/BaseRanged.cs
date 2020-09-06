using System;
using Server.Network;

namespace Server.Items
{
    public abstract class BaseRanged : BaseMeleeWeapon
    {
        public virtual int EffectID { get; set;  }
        public abstract Type AmmoType { get; }
        public abstract Item Ammo { get; }

        public override int DefaultHitSound
        {
            get { return 0x234; }
        }

        public override int DefaultMissSound
        {
            get { return 0x238; }
        }

        public override SkillName DefaultSkill
        {
            get { return SkillName.Archery; }
        }

        public override WeaponType DefaultWeaponType
        {
            get { return WeaponType.Ranged; }
        }

        public override WeaponAnimation DefaultAnimation
        {
            get { return WeaponAnimation.ShootXBow; }
        }

        public override SkillName AccuracySkill
        {
            get { return SkillName.Archery; }
        }

        private bool m_Balanced;
        private int m_Velocity;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Balanced
        {
            get { return m_Balanced; }
            set { m_Balanced = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Velocity
        {
            get { return m_Velocity; }
            set { m_Velocity = value; }
        }

        public BaseRanged(int itemID) : base(itemID)
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
                    attacker.Send(new Swing(attacker.Serial, defender.Serial));

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

            attacker.MovingEffect(defender, EffectID, 18, 1, false, false);

            return true;
        }

        public override void Serialize(IGenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int) 3); // version

            writer.Write((bool) m_Balanced);
            writer.Write((int) m_Velocity);
        }

        public override void Deserialize(IGenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 3:
                {
                    m_Balanced = reader.ReadBool();
                    m_Velocity = reader.ReadInt();

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
