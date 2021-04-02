using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Fourth
{
    public class ManaDrainSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public ManaDrainSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            target.Spell?.OnCasterHurt();
            target.Paralyzed = false;
            
            var magery = Caster.Skills[SkillName.Magery].Value;
            var resist = target.Skills[SkillName.MagicResist].Value;

            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref magery));

            string message;
            if (resist >= magery + 5)
                message = $"{target.Name}'s will is too strong!";
            else
            {
                var amount = SpellHelper.GetDamageAfterResist(Caster, target, magery);
                var mana = target.Mana - amount;
                target.Mana = mana > 0 ? mana : 0;
                
                message = $"You expelled {amount} mana out of {target.Name}'s aura!";
            }

            target.PrivateOverheadMessage(MessageType.Regular, 0x3B2, true, message, Caster.NetState);
            
            target.FixedParticles(0x374A, 10, 15, 5032, EffectLayer.Head);
            target.PlaySound(0x1F8);
        }
    }
}