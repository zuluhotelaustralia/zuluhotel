using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Sixth
{
    public class MassCurseSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public MassCurseSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;
            
            var target = SpellHelper.GetSurfaceTop(response.Target);

            var range = 4.0;
            Caster.FireHook(h => h.OnModifyWithMagicEfficiency(Caster, ref range));

            var eable = Caster.Map.GetMobilesInRange(target, (int)range);
            foreach (var mobile in eable)
            {
                if (!Caster.CanBuff(mobile, false, BuffIcon.Curse, BuffIcon.Bless))
                    return;

                Caster.DoHarmful(mobile);
                mobile.TryAddBuff(new StatBuff(StatType.All)
                {
                    Value = SpellHelper.GetModAmount(Caster, mobile, StatType.All) * -1,
                    Duration = SpellHelper.GetDuration(Caster, mobile),
                });

                mobile.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
                mobile.PlaySound(0x1FB);
            }
            eable.Free();
        }
    }
}