using System.Threading.Tasks;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enchantments.Buffs;

namespace Server.Spells.Third
{
    public class BlessSpell : MagerySpell, ITargetableAsyncSpell<Mobile>
    {
        public BlessSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<Mobile> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            if (!Caster.CanBuff(target, BuffIcon.Bless))
                return;
            
            target.TryAddBuff(new StatBuff(StatType.All)
            {
                Value = SpellHelper.GetModAmount(Caster, target, StatType.All),
                Duration = SpellHelper.GetDuration(Caster, target),
            });
            SpellHelper.Turn(Caster, target);

            target.FixedParticles(0x373A, 10, 15, 5018, EffectLayer.Waist);
            target.PlaySound(0x1EA);
        }
    }
}