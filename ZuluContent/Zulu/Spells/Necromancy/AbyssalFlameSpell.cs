using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Server;
using Server.Engines.Magic;
using Server.Network;
using Server.Items;
using Server.Spells;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Scripts.Zulu.Spells.Necromancy
{
    public class AbyssalFlameSpell : NecromancerSpell, ITargetableAsyncSpell<Mobile>
    {
        public AbyssalFlameSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);
            
            Caster.MovingEffect(target, 0x36D5, 5, 0, false, false);
            var damage = SpellHelper.CalcSpellDamage(Caster, target, this);
            SpellHelper.Damage(damage, target, Caster, this);

            await Timer.Pause(500);
            
            var range = Caster.Skills[CastSkill].Value / 15.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));

            var eable = Caster.GetMobilesInRange((int) range);
            foreach (var mobile in eable)
            {
                if (Caster == mobile || 
                    !SpellHelper.ValidIndirectTarget(Caster, mobile) ||
                    !Caster.CanBeHarmful(mobile, false) || 
                    !Caster.InLOS(mobile))
                {
                    continue;
                }
                
                SpellHelper.Damage(damage / 2, mobile, Caster, this);
                mobile.FixedParticles(0x36BD, 20, 10, 5030, EffectLayer.Head);
                mobile.PlaySound(0x307);
            }
            eable.Free();
        }
    }
}