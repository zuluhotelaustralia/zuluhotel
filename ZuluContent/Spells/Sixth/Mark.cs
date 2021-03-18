using System.Threading.Tasks;
using Server.Items;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Sixth
{
    public class MarkSpell : MagerySpell, ITargetableAsyncSpell<RecallRune>
    {
        public MarkSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<RecallRune> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;
            
            if (!SpellHelper.CheckTravel(Caster, TravelCheckType.Mark))
                return;

            if (SpellHelper.CheckMulti(Caster.Location, Caster.Map, true))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
                return;
            }
            
            if (!target.IsChildOf(Caster.Backpack))
            {
                // You must have this rune in your backpack in order to mark it.
                Caster.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1062422);
                return;
            }
            
            target.Mark(Caster);

            Caster.PlaySound(0x1FA);
            Effects.SendLocationEffect(Caster, 14201, 16);
        }
    }
}