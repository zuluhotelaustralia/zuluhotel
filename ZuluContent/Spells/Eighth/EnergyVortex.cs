using System;
using System.Threading.Tasks;
using Server.Mobiles;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Eighth
{
    public class EnergyVortexSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public EnergyVortexSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);
            SpellHelper.Turn(Caster, point);
            
            var duration = Caster.Skills[SkillName.Magery].Value / 3.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref duration));
            
            BaseCreature.Summon(Creatures.Create("EnergyVortex"), false, Caster, point, 0x212, TimeSpan.FromSeconds(duration));
        }
    }
}