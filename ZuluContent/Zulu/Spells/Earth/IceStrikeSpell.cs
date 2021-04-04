using System;
using System.Collections;
using System.Threading.Tasks;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Scripts.Zulu.Spells.Earth
{
    public class IceStrikeSpell : EarthSpell, ITargetableAsyncSpell<Mobile>
    {
        public IceStrikeSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            SpellHelper.Turn(Caster, target);
            
            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            
            target.FixedParticles(0x3789, 30, 30, 5028, EffectLayer.Waist);
            target.PlaySound(0x0116);
            target.PlaySound(0x0117);
            
            SpellHelper.Damage(damage, target, Caster, this);
        }
    }
}