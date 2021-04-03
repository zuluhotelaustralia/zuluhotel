using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Network;
using Server.Targeting;
using ZuluContent.Multis;

namespace Server.Spells.Sixth
{
    public class MarkSpell : MagerySpell, ITargetableAsyncSpell<RecallRune>
    {
        public MarkSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<RecallRune> response)
        {
            if (!response.HasValue)
                return;
            
            var target = response.Target;

            if (!SpellHelper.CheckTravel(Caster, TravelCheckType.Mark))
            {
                // Thy spell doth not appear to work...
                Caster.PrivateOverheadMessage(MessageType.Regular, 0x3B2, 501802, Caster.NetState);
                return;
            }

            if (Caster.GetMulti()?.IsMultiOwner(Caster) == false)
            {
                Caster.SendFailureMessage(1010587); // You are not a co-owner of this house.
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