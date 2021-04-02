using System;
using System.Collections;
using System.Threading.Tasks;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Mobiles;
using Server.Spells;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Earth
{
    public class ShiftingEarthSpell : EarthSpell, ITargetableAsyncSpell<Mobile>
    {
        public override TimeSpan CastDelayBase => TimeSpan.FromSeconds(0);
    
        public override double RequiredSkill => 60.0;
    
        public override int RequiredMana => 5;

        public ShiftingEarthSpell(Mobile caster, Item spellItem) : base(caster, spellItem)
        {
        }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            
            target.FixedParticles(0x3789, 10, 10, 5028, EffectLayer.Waist);
            target.PlaySound(0x020D);
            
            SpellHelper.Damage(damage, target, Caster, this);
            
            if (!Caster.CanBuff(target, true, BuffIcon.Clumsy, BuffIcon.Agility))
                return;

            target.TryAddBuff(new StatBuff(StatType.Dex)
            {
                Value = SpellHelper.GetModAmount(Caster, target, StatType.Dex) * -1,
                Duration = SpellHelper.GetDuration(Caster, target),
            });
        }
    }
}