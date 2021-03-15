using System.Collections;
using System.Threading.Tasks;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Fifth
{
    public class MagicReflectSpell : MagerySpell, IAsyncSpell
    {
        public MagicReflectSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task CastAsync()
        {
            if (!Caster.CanBuff(Caster, BuffIcon.MagicReflection))
            {
                Caster.SendLocalizedMessage(1005559);
                return;
            }

            Caster.TryAddBuff(new MagicReflection());
            
            // Caster.FixedParticles(0x375A, 10, 15, 5037, EffectLayer.Waist);
            // Caster.PlaySound(0x1E9);
            Caster.FixedEffect(0x374B, 10, 10);
            Caster.PlaySound(0x1E7);
        }
    }
}