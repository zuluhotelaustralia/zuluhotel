using System.Collections;
using System.Threading.Tasks;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Fifth
{
    public class MagicReflectSpell : MagerySpell, IAsyncSpell
    {
        public MagicReflectSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            if (!Caster.CanBuff(Caster, BuffIcon.MagicReflection, false))
            {
                Caster.SendLocalizedMessage(1005559); // This spell is already in effect.
                return;
            }

            var value = Caster.Skills[SkillName.Magery].Value / 12.0 + 1.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref value));

            Caster.TryAddBuff(new MagicReflection
            {
                Circle = value >= (int)SpellCircle.System 
                    ? (SpellCircle)(int)SpellCircle.System - 1 
                    : (SpellCircle)(int)value
            });
            
            Caster.FixedParticles(0x374B, 10, 10, 5037, EffectLayer.Waist);
            Caster.PlaySound(0x1E7);
        }
    }
}