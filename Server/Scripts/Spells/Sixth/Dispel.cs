using System;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.Sixth
{
    public class DispelSpell : MagerySpell
    {
        private static SpellInfo m_Info = new SpellInfo(
                                "Dispel", "An Ort",
                                218,
                                9002,
                                Reagent.Garlic,
                                Reagent.MandrakeRoot,
                                Reagent.SulfurousAsh
                                );

        public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

        public DispelSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            Caster.Target = new InternalTarget(this);
        }

        public class InternalTarget : Target
        {
            private DispelSpell m_Owner;

            public InternalTarget(DispelSpell owner) : base(Core.ML ? 10 : 12, false, TargetFlags.Harmful)
            {
                m_Owner = owner;
            }

            protected override void OnTarget(Mobile from, object o)
            {
                // daleron: not sure if this can ever happen, but the
                // original code had it so sure.
                if (!(o is Mobile))
                    return;

                Mobile m = (Mobile)o;
                BaseCreature bc = m as BaseCreature;

                // daleron: If we can't see the target, just abort.
                if (!from.CanSee(m))
                {
                    from.SendLocalizedMessage(500237); // Target can not be seen.
                    return;
                }


                // daleron: Checks for fizzling, sufficient mana,
                // reagents, scroll consumption, wand usage, etc.


                // daleorn: This used to be CheckHSequence, but that
                // means it would be a criminal action to dispell your
                // buddies curses.  If we were really clever we'd
                // check if the target was cursed rather than buffed.
                // So changed to CheckSequence for now.
                if (!m_Owner.CheckSequence())
                    return;

                // daleron: Turn to the target and play the vfx/sfx
                // regardless of whether we succeed/fail on the spell.

                SpellHelper.Turn(from, m);
                Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                Effects.PlaySound(m, m.Map, 0x201);


                // daleron: If the target was a creature and they are
                // dispellable (like a summon) then we attempt to
                // dispell the creature.
                if (bc != null && bc.IsDispellable)
                {
                    double dispelChance = (50.0 + ((100 * (from.Skills.Magery.Value - bc.DispelDifficulty)) / (bc.DispelFocus * 2))) / 100;

                    if (dispelChance > Utility.RandomDouble())
                    {
                        Effects.SendLocationParticles(EffectItem.Create(m.Location, m.Map, EffectItem.DefaultDuration), 0x3728, 8, 20, 5042);
                        Effects.PlaySound(m, m.Map, 0x201);

                        m.Delete();
                    }
                    else
                    {
                        m.FixedEffect(0x3779, 10, 20);
                        from.SendLocalizedMessage(1010084); // The creature resisted the attempt to dispel it!
                    }

                    return;
                }
                // daleron: The target was not a summoned creature so we will attempt to remove they buffs/curses.


                // daleron: If we're not casting on ourselves, check
                // reflection/resisting.
                if (from != m)
                {
                    // daleron: CheckRefelct will swap the values of from/m if successful.                    
                    SpellHelper.CheckReflect((int)m_Owner.Circle, from, ref m);

                    // daleron: Then we check if the new target resisted the spell.
                    if (m_Owner.CheckResisted(m))
                    {
                        // In which case, send them a message and stop processing the spell

                        m.SendLocalizedMessage(501783); // You feel yourself resisting magical energy
                        return;
                    }
                }

                // if the buff is applied by SpellHelper it prepends "[Magic]" to the statmod's name
                // so we can hopefully safely assume this is a magic buff and not e.g. a ring of +25 dex
                if (m.IsBodyMod || m.HueMod != -1 || !(m.NameMod == null))
                {
                    m.BodyMod = 0;
                    m.HueMod = -1;
                    m.NameMod = null;

                    m.EndAction(typeof(Spells.Necromancy.WraithFormSpell));
                    m.EndAction(typeof(Spells.Necromancy.LicheFormSpell));
                    m.EndAction(typeof(Spells.Seventh.PolymorphSpell));
                    m.EndAction(typeof(Spells.Earth.ShapeshiftSpell));
                    m.EndAction(typeof(Spells.Fifth.IncognitoSpell));
                }

                if (m.StatMods != null)
                {
                    foreach (StatMod mod in m.StatMods.ToArray())
                    {
                        // daleron: Jesus fucking christ we're
                        // checking if it has "magic" in the name?
                        // Is this sphereserver?  Should we stuff
                        // a hidden item into a MORE2 variable?

                        if (mod.Name.Contains("magic", StringComparison.OrdinalIgnoreCase))
                        {
                            // get yeeted on
                            m.RemoveStatMod(mod.Name);
                        }
                    }
                }// foreach
            }

            protected override void OnTargetFinish(Mobile from)
            {
                m_Owner.FinishSequence();
            }
        }
    }
}
