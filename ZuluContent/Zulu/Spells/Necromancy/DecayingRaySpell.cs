using System;
using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server;
using Server.Network;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class DecayingRaySpell : NecromancerSpell, ITargetableAsyncSpell<Mobile>
    {
        public DecayingRaySpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);

            if (!Caster.CanBuff(target, true, BuffIcon.Protection, BuffIcon.HitLowerDefense))
                return;

            var amount = SpellHelper.GetModAmount(Caster, target) * 1.25;
            var duration = SpellHelper.GetDuration(Caster, target).TotalSeconds;
            
            var protection = target.GetResist(Info.DamageType);
            if (protection > 0)
            {
                var modifier = 100.0 - protection;
                duration *= modifier / 100;
                amount *= modifier / 100;
                
                if (duration < 1 || amount < 1)
                {
                    target.PrivateOverheadMessage(
                        MessageType.Regular, 
                        0x3B2, 
                        true, 
                        "The nature of the target prevents it from being affected by that spell!", 
                        Caster.NetState
                    );
                    return;
                }
            }

            amount = SpellHelper.TryResistDamage(Caster, target, Circle, (int) amount);
            duration = SpellHelper.TryResistDamage(Caster, target, Circle, (int) duration);

            if (amount < 1)
                return;
            
            target.TryAddBuff(new ArmorBuff
            {
                Icon = BuffIcon.HitLowerDefense,
                Title = "Decaying Ray",
                ArmorMod = (int)amount * -1,
                Duration = TimeSpan.FromSeconds(duration),
            });
            
            if(Caster != target)
                Caster.SendSuccessMessage($"You reduced {target.Name}'s armor rating by {amount}.");
            
            target.SendSuccessMessage($"Your armor rating was reduced by {amount}.");
            target.PlaySound(0xFD);
        }
    }
}