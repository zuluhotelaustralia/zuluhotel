using Server.Items;
using Server.Targeting;

namespace Server.Spells.Second
{
    public class RemoveTrapSpell : MagerySpell
    {
        public RemoveTrapSpell(Mobile caster, Item scroll) : base(caster, scroll)
        {
        }


        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
            Caster.SendMessage("What do you wish to untrap?");
        }

        public void Target(TrapableContainer item)
        {
            if (!Caster.CanSee(item))
            {
                Caster.SendLocalizedMessage(500237); // Target can not be seen.
            }
            else if (item.TrapType != TrapType.None && item.TrapType != TrapType.MagicTrap)
            {
                base.DoFizzle();
            }
            else if (CheckSequence())
            {
                SpellHelper.Turn(Caster, item);

                var loc = item.GetWorldLocation();

                Effects.SendLocationParticles(EffectItem.Create(loc, item.Map, EffectItem.DefaultDuration), 0x376A, 9,
                    32, 5015);
                Effects.PlaySound(loc, item.Map, 0x1F0);

                item.TrapType = TrapType.None;
                item.TrapPower = 0;
                item.TrapLevel = 0;
            }

            FinishSequence();
        }

        private class InternalTarget : Target
        {
            private readonly RemoveTrapSpell m_Owner;

            public InternalTarget(RemoveTrapSpell owner) : base(12, false, TargetFlags.None)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                if (o is TrapableContainer)
                    m_Owner.Target((TrapableContainer) o);
                else
                    @from.SendMessage("You can't disarm that");
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}