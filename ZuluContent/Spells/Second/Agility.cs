using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Second
{
    public class AgilitySpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public AgilitySpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue || !(response.Target is IBuffable buffable))
                return;

            if (!buffable.BuffManager.CanBuffWithNotifyOnFail<Agility>(Caster))
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);

            buffable.BuffManager.AddBuff(new Agility
            {
                Value = SpellHelper.GetModAmount(Caster, target, StatType.Dex),
                Duration = SpellHelper.GetDuration(Caster, target),
            });

            target.FixedParticles(0x375A, 10, 15, 5010, EffectLayer.Waist);
            target.PlaySound(0x1e7);
        }
    }
}