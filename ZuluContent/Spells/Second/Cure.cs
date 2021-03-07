using System.Threading.Tasks;
using Scripts.Zulu.Engines.Classes;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Second
{
    public class CureSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public CureSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var m = response.Target;
            
            SpellHelper.Turn(Caster, m);

            var p = m.Poison;
            if (p is null)
                return;
            
            double difficulty = p.Level * 15 + 60;
            Caster.FireHook(h => h.OnCure(Caster, m, p, this, ref difficulty));

            if (difficulty < 10)
                difficulty = 10;
            
            if (Caster.ShilCheckSkill(SkillName.Magery, (int)difficulty, 0) && m.CurePoison(Caster))
            {
                if (Caster != m)
                {
                    Caster.SendLocalizedMessage(1010058); // You have cured the target of all poisons!
                    m.SendLocalizedMessage(1010059);  // You have been cured of all poisons.
                }
                else
                {
                    Caster.SendLocalizedMessage(1010059);  // You have been cured of all poisons.
                }
            }
            else
            {
                m.SendLocalizedMessage(1010060); // You have failed to cure your target!
            }

            m.FixedParticles(0x373A, 10, 15, 5012, EffectLayer.Waist);
            m.PlaySound(0x1E0);
        }
    }
}