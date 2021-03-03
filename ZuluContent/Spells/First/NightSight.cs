using Server.Targeting;

namespace Server.Spells.First
{
    public class NightSightSpell : MagerySpell
    {
        public NightSightSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override void OnCast()
        {
            Caster.Target = new NightSightTarget(this);
        }

        private class NightSightTarget : Target
        {
            private readonly Spell m_Spell;

            public NightSightTarget(Spell spell) : base(12, false, TargetFlags.Beneficial)
            {
                m_Spell = spell;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Mobile && m_Spell.CheckBSequence((Mobile) targeted))
                {
                    var targ = (Mobile) targeted;

                    SpellHelper.Turn(m_Spell.Caster, targ);

                    if (targ.BeginAction(typeof(LightCycle)))
                    {
                        new LightCycle.NightSightTimer(targ).Start();
                        var level = (int) (LightCycle.DungeonLevel * (from.Skills[SkillName.Magery].Value / 100));

                        if (level < 0)
                            level = 0;

                        targ.LightLevel = level;

                        targ.FixedParticles(0x376A, 9, 32, 5007, EffectLayer.Waist);
                        targ.PlaySound(0x1E3);
                    }
                    else
                    {
                        from.SendMessage("{0} already have nightsight.", from == targ ? "You" : "They");
                    }
                }

                m_Spell.FinishSequence();
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Spell.FinishSequence();
            }
        }
    }
}