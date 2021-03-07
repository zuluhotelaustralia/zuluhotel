using System.Threading.Tasks;
using Server.Items;
using Server.Targeting;

namespace Server.Spells.Second
{
    public class RemoveTrapSpell : MagerySpell, ITargetableAsyncSpell<TrapableContainer>
    {
        public RemoveTrapSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }

        public async Task OnTargetAsync(ITargetResponse<TrapableContainer> response)
        {
            if (!response.HasValue)
                return;

            var item = response.Target;
            
            if (item.TrapType != TrapType.None && item.TrapType != TrapType.MagicTrap)
            {
                base.DoFizzle();
                return;
            }

            SpellHelper.Turn(Caster, item);

            var loc = item.GetWorldLocation();

            Effects.SendLocationParticles(EffectItem.Create(loc, item.Map, EffectItem.DefaultDuration), 0x376A, 9,
                32, 5015);
            Effects.PlaySound(loc, item.Map, 0x1F0);

            item.TrapType = TrapType.None;
            item.TrapStrength = 0;
            item.TrapLevel = 0;
        }
    }
}