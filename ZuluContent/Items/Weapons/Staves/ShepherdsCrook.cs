using System;
using Scripts.Zulu.Engines.Classes;
using Scripts.Zulu.Utilities;
using Server.Network;
using Server.Targeting;
using Server.Mobiles;

namespace Server.Items
{
    [FlipableAttribute(0xE81, 0xE82)]
    public class ShepherdsCrook : BaseStaff
    {
        public override int DefaultMinDamage => 3;
        public override int DefaultMaxDamage => 12;
        public override int DefaultSpeed => 30;
        public override int InitMinHits => 70;
        public override int InitMaxHits => 70;


        [Constructible]
        public ShepherdsCrook() : base(0xE81)
        {
            Weight = 4.0;
            Layer = Layer.TwoHanded;
        }

        [Constructible]
        public ShepherdsCrook(Serial serial) : base(serial)
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

            if (Weight == 2.0)
                Weight = 4.0;
        }

        public override void OnDoubleClick(Mobile from)
        {
            from.SendSuccessMessage(502464); // Target the animal you wish to herd.
            from.Target = new HerdingTarget();
        }

        private class HerdingTarget : Target
        {
            public HerdingTarget() : base(10, false, TargetFlags.None)
            {
            }

            protected override void OnTarget(Mobile from, object targ)
            {
                if (targ is BaseCreature bc)
                {
                    if (IsHerdable(bc))
                    {
                        if (bc.Controlled)
                        {
                            bc.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502467,
                                from.NetState); // That animal looks tame already.
                        }
                        else
                        {
                            from.SendSuccessMessage(502475); // Click where you wish the animal to go.
                            from.Target = new InternalTarget(bc);
                        }
                    }
                    else
                    {
                        from.SendFailureMessage(502468); // That is not a herdable animal.
                    }
                }
                else
                {
                    from.SendFailureMessage(502472); // You don't seem to be able to persuade that to move.
                }
            }

            private bool IsHerdable(BaseCreature bc)
            {
                if (bc.Tamable)
                    return true;

                return false;
            }

            private class InternalTarget : Target
            {
                private BaseCreature m_Creature;

                public InternalTarget(BaseCreature c) : base(10, true, TargetFlags.None)
                {
                    m_Creature = c;
                }

                protected override void OnTarget(Mobile from, object targ)
                {
                    if (targ is IPoint2D targetedPoint)
                    {
                        var difficulty = m_Creature.MinTameSkill;

                        if (difficulty == 0)
                            difficulty = 100;
                        
                        difficulty /= from.GetClassModifier(SkillName.Herding);

                        if (difficulty <= from.Skills[SkillName.Herding].Value)
                            m_Creature.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 502471,
                                from.NetState); // That wasn't even challenging.

                        if (from.ShilCheckSkill(SkillName.Herding, (int) difficulty, (int) (difficulty * 10)))
                        {
                            var p = targetedPoint;

                            if (targ != from)
                                p = new Point2D(p.X, p.Y);

                            m_Creature.TargetLocation = p;
                            from.SendSuccessMessage(502479); // The animal walks where it was instructed to.
                        }
                        else
                        {
                            from.SendFailureMessage(502472); // You don't seem to be able to persuade that to move.
                        }
                    }
                }
            }
        }
    }
}