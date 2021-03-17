using System;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Second
{
    public class CunningSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public CunningSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            if (!Caster.CanBuff(target, BuffIcon.Cunning))
                return;
            
            target.TryAddBuff(new StatBuff(StatType.Int)
            {
                Value = SpellHelper.GetModAmount(Caster, target, StatType.Int),
                Duration = SpellHelper.GetDuration(Caster, target),
            });
            
            SpellHelper.Turn(Caster, target);

            target.FixedParticles(0x375A, 10, 15, 5011, EffectLayer.Head);
            target.PlaySound(0x1EB);
        }
    }
}