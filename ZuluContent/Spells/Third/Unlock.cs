using Server.Items;
using Server.Multis;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Third
{
    public class UnlockSpell : MagerySpell
    {
        public UnlockSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        private class InternalTarget : Target
        {
            private readonly UnlockSpell m_Owner;

            public InternalTarget(UnlockSpell owner) : base(12, false, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                var loc = o as IPoint3D;

                if (loc == null)
                    return;

                if (m_Owner.CheckSequence())
                {
                    SpellHelper.Turn(from, o);

                    Effects.SendLocationParticles(
                        EffectItem.Create(new Point3D(loc), from.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 5024);

                    Effects.PlaySound((Point3D)loc, from.Map, 0x1FF);

                    if (o is Mobile)
                    {
                        @from.LocalOverheadMessage(MessageType.Regular, 0x3B2,
                            503101); // That did not need to be unlocked.
                    }
                    else if (!(o is LockableContainer))
                    {
                        @from.SendLocalizedMessage(501666); // You can't unlock that!
                    }
                    else
                    {
                        var cont = (LockableContainer) o;

                        if (BaseHouse.CheckSecured(cont))
                        {
                            @from.SendLocalizedMessage(503098); // You cannot cast this on a secure item.
                        }
                        else if (!cont.Locked)
                        {
                            @from.LocalOverheadMessage(MessageType.Regular, 0x3B2,
                                503101); // That did not need to be unlocked.
                        }
                        else if (cont.LockLevel == 0)
                        {
                            @from.SendLocalizedMessage(501666); // You can't unlock that!
                        }
                        else
                        {
                            var level = (int) (from.Skills[SkillName.Magery].Value * 0.8) - 4;

                            if (level >= cont.RequiredSkill &&
                                !(cont is TreasureMapChest && ((TreasureMapChest) cont).Level > 2))
                            {
                                cont.Locked = false;

                                if (cont.LockLevel == -255)
                                    cont.LockLevel = cont.RequiredSkill - 10;
                            }
                            else
                            {
                                @from.LocalOverheadMessage(MessageType.Regular, 0x3B2,
                                    503099); // My spell does not seem to have an effect on that lock.
                            }
                        }
                    }
                }

                m_Owner.FinishSequence();
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}