using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Fourth
{
    public class ArchProtectionSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public ArchProtectionSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var target = SpellHelper.GetSurfaceTop(response.Target);
            
            SpellHelper.Turn(Caster, target);
            var targets = new List<Mobile>();

            IPooledEnumerable eable = Caster.Map.GetMobilesInRange(target, 3);
            targets.AddRange(eable.Cast<Mobile>().Where(m => Caster.CanBeBeneficial(m, false)));
            eable.Free();

            Effects.PlaySound(target, Caster.Map, 0x299);

            if (targets.Count == 0)
                return;

            foreach (var mobile in targets)
            {
                if (mobile is IBuffable buffable && !buffable.BuffManager.HasBuff<Protection>())
                {
                    Caster.DoBeneficial(mobile);
                    
                    buffable.BuffManager.AddBuff(new Protection
                    {
                        Value = (int) (SpellHelper.GetModAmount(Caster, mobile) / 1.5),
                        Duration = SpellHelper.GetDuration(Caster, mobile),
                    });
                    
                    mobile.FixedParticles(0x375A, 9, 20, 5027, EffectLayer.Waist);
                    mobile.PlaySound(0x1F7);
                }
            }
        }
    }
}