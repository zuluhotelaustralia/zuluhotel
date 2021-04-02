using System;
using System.Threading.Tasks;
using Server.Engines.Magic;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Seventh
{
    public class ChainLightningSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public ChainLightningSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;
            
            var point = SpellHelper.GetSurfaceTop(response.Target);
            
            var range = Caster.Skills[SkillName.Magery].Value / 15.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));
            
            var eable = Caster.Map.GetMobilesInRange(point, (int)range);
            foreach (var target in eable)
            {
                if (!SpellHelper.ValidIndirectTarget(Caster, target) || 
                    !Caster.CanBeHarmful(target, false) ||
                    !Caster.InLOS(target)
                )
                    continue;

                var damage = SpellHelper.CalcSpellDamage(Caster, target, this, true);
                target.BoltEffect(0);
                SpellHelper.Damage(damage, target, Caster, this, TimeSpan.Zero, ElementalType.Air);
            }
            eable.Free();

            Caster.PlaySound(0x29);
        }
    }
}