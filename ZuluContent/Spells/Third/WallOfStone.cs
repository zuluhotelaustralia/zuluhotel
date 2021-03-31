using System;
using System.Threading.Tasks;
using Server.Items;
using Server.Misc;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;

namespace Server.Spells.Third
{
    public class WallOfStoneSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public WallOfStoneSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);
            
            SpellHelper.Turn(Caster, point);
            
            Effects.PlaySound(new Point3D(point), Caster.Map, 0x1F6);
            
            var durationSeconds = Caster.Skills[SkillName.Magery].Value * 3.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref durationSeconds));
            var duration = TimeSpan.FromSeconds(durationSeconds);

            FieldItem.CreateField(
                (0x58, 0x57),
                point,
                Caster,
                duration,
                TimeSpan.Zero,
                onCreate: item => Effects.SendLocationParticles(item, 0x376A, 9, 10, 5025)
            );
        }
    }
}