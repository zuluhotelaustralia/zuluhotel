using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Fourth
{
    public class CurseSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public CurseSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue || !(response.Target is IBuffable buffable))
                return;

            if (!buffable.BuffManager.CanBuffWithNotifyOnFail<Bless>(Caster))
                return;
            
            var target = response.Target;
            SpellHelper.Turn(Caster, target);

            buffable.BuffManager.AddBuff(new Bless
            {
                Value = SpellHelper.GetModAmount(Caster, target, StatType.All) * -1,
                Duration = SpellHelper.GetDuration(Caster, target),
            });
            
            target.Spell?.OnCasterHurt();
            target.Paralyzed = false;

            target.FixedParticles(0x374A, 10, 15, 5028, EffectLayer.Waist);
            target.PlaySound(0x1E1);
        }
    }
}