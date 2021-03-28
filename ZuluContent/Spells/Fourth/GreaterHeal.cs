using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Fourth
{
    public class GreaterHealSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public GreaterHealSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;

            var target = response.Target;

            SpellHelper.Turn(Caster, target);
            var healed = Utility.Random(3, 18) + Caster.Skills[SkillName.Magery].Value / 4;
            
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref healed));
            
            if (target is BaseCreature {CreatureType: CreatureType.Undead})
                SpellHelper.Damage((int) healed, target, Caster, this);
            else
            {
                SpellHelper.Heal((int) healed, target, Caster, this);
                Caster.SendSuccessMessage($"You healed {(int) healed} damage.");
            }

            target.FixedParticles(0x376A, 9, 32, 5030, EffectLayer.Waist);
            target.PlaySound(0x202);
        }
    }
}