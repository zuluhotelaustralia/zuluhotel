using Server.Items;
using Server.Multis;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Third
{
    public class MagicLockSpell : MagerySpell
    {
        public MagicLockSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public void Target(LockableContainer targ)
        {
            if (BaseHouse.CheckLockedDownOrSecured(targ))
            {
                // You cannot cast this on a locked down item.
                Caster.LocalOverheadMessage(MessageType.Regular, 0x22, 501761);
            }
            else if (targ.Locked || targ.LockLevel == 0)
            {
                // Target must be an unlocked chest.
                Caster.SendLocalizedMessage(501762);
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, targ);

                var loc = targ.GetWorldLocation();

                Effects.SendLocationParticles(
                    EffectItem.Create(loc, targ.Map, EffectItem.DefaultDuration),
                    0x376A, 9, 32, 5020);

                Effects.PlaySound(loc, targ.Map, 0x1FA);

                // The chest is now locked!
                Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 501763);

                targ.LockLevel = -255; // signal magic lock
                targ.Locked = true;
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly MagicLockSpell m_Owner;

            public InternalTarget(MagicLockSpell owner) : base(12, false, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is LockableContainer)
                    m_Owner.Target((LockableContainer) o);
                else
                    from.SendLocalizedMessage(501762); // Target must be an unlocked chest.
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}