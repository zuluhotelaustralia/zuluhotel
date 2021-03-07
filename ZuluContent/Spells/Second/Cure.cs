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

            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);

            var poison = target.Poison;
            if (poison is null)
                return;
            
            double difficulty = poison.Level * 15 + 60;
            Caster.FireHook(h => h.OnCure(Caster, target, poison, this, ref difficulty));

            if (difficulty < 10)
                difficulty = 10;
            
            if (Caster.ShilCheckSkill(SkillName.Magery, (int)difficulty, 0) && target.CurePoison(Caster))
            {
                if (Caster != target)
                {
                    Caster.SendLocalizedMessage(1010058); // You have cured the target of all poisons!
                    target.SendLocalizedMessage(1010059);  // You have been cured of all poisons.
                }
                else
                {
                    Caster.SendLocalizedMessage(1010059);  // You have been cured of all poisons.
                }
            }
            else
            {
                target.SendLocalizedMessage(1010060); // You have failed to cure your target!
            }

            target.FixedParticles(0x373A, 10, 15, 5012, EffectLayer.Waist);
            target.PlaySound(0x1E0);
        }
    }
}