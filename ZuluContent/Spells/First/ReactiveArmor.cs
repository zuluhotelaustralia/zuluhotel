using System.Collections;

namespace Server.Spells.First
{
    public class ReactiveArmorSpell : MagerySpell
    {
        private static readonly Hashtable m_Table = new Hashtable();

        public ReactiveArmorSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }


        public override bool CanCast()
        {
            if (!base.CanCast())
                return false;
            
            if (Caster.MeleeDamageAbsorb > 0)
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                return false;
            }

            if (!Caster.CanBeginAction(typeof(DefensiveSpell)))
            {
                Caster.SendLocalizedMessage(1005385); // The spell will not adhere to you at this time.
                return false;
            }

            return true;
        }

        public override void OnCast()
        {
            if (Caster.MeleeDamageAbsorb > 0)
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
            }
            else if (!Caster.CanBeginAction(typeof(DefensiveSpell)))
            {
                Caster.SendLocalizedMessage(1005385); // The spell will not adhere to you at this time.
            }
            else if (CheckSequence())
            {
                if (Caster.BeginAction(typeof(DefensiveSpell)))
                {
                    var value = (int) (Caster.Skills[SkillName.Magery].Value +
                                       Caster.Skills[SkillName.Meditation].Value +
                                       Caster.Skills[SkillName.Inscribe].Value);
                    value /= 3;

                    if (value < 0)
                        value = 1;
                    else if (value > 75)
                        value = 75;

                    Caster.MeleeDamageAbsorb = value;

                    Caster.FixedParticles(0x376A, 9, 32, 5008, EffectLayer.Waist);
                    Caster.PlaySound(0x1F2);
                }
                else
                {
                    Caster.SendLocalizedMessage(1005385); // The spell will not adhere to you at this time.
                }
            }

            FinishSequence();
        }

        public static void EndArmor(Mobile m)
        {
            if (m_Table.Contains(m)) m_Table.Remove(m);
        }
    }
}