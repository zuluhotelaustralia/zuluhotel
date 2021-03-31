using System.Threading.Tasks;
using Server.Items;
using Server.Targeting;


namespace Server
{
    public interface ITelekinesisable : IPoint3D
    {
        void OnTelekinesis(Mobile from);
    }
}

namespace Server.Spells.Third
{
    public class TelekinesisSpell : MagerySpell, ITargetableAsyncSpell<Item>
    {
        public TelekinesisSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        public async Task OnTargetAsync(ITargetResponse<Item> response)
        {
            if (!response.HasValue)
                return;

            var item = response.Target;

            SpellHelper.Turn(Caster, item);

            if (item is ITelekinesisable tele)
            {
                tele.OnTelekinesis(Caster);
                return;
            }
            
            var root = item.RootParent;

            if (!item.IsAccessibleTo(Caster))
            {
                item.OnDoubleClickNotAccessible(Caster);
                return;
            }
            
            if (!item.CheckItemUse(Caster, item))
            {
                Caster.SendLocalizedMessage(502218); // You cannot use that. 
                return;
            }
            
            if (root is Mobile && root != Caster)
            {
                item.OnSnoop(Caster);
                return;
            }
            
            if (item is Corpse corpse && !corpse.CheckLoot(Caster, null))
            {
                return;
            }
            
            if (Caster.Region.OnDoubleClick(Caster, item))
            {
                Effects.SendLocationParticles(
                    EffectItem.Create(item.Location, item.Map, EffectItem.DefaultDuration), 0x376A, 9, 32, 5022);
                Effects.PlaySound(item.Location, item.Map, 0x1F5);

                item.OnItemUsed(Caster, item);
            }
        }
    }
}