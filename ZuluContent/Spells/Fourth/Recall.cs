using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Server.Items;
using Server.Mobiles;
using Server.Multis;
using Server.Targeting;
using ZuluContent.Multis;

namespace Server.Spells.Fourth
{
    public class RecallSpell : MagerySpell, ITargetableAsyncSpell<Item>
    {
        private readonly Runebook m_Book;
        private readonly RunebookEntry m_Entry;

        [SuppressMessage("ReSharper", "RedundantOverload.Global")]
        public RecallSpell(Mobile caster, Item spellItem) : this(caster, spellItem, null)
        {
            
        }

        public RecallSpell(Mobile caster, Item spellItem, RunebookEntry entry = null, Runebook book = null)
            : base(caster, spellItem)
        {
            m_Entry = entry;
            m_Book = book;
        }

        /**
         * We override the default interface method from ITargetableAsyncSpell<Item>.CastAsync to conditionally
         * skip targeting as the Runebook gump will provide the necessary Book/Entry via the constructor.
         */
        async Task IAsyncSpell.CastAsync()
        {
            if (m_Book == null && m_Entry == null)
                await (this as ITargetableAsyncSpell<Item>).SendTargetAsync();
            else
                Effect(m_Entry.Location, m_Entry.Map);
        }

        public async Task OnTargetAsync(ITargetResponse<Item> response)
        {
            if (!response.HasValue)
                return;
            
            switch (response.Target)
            {
                case RecallRune rune:
                {
                    if (rune.Marked)
                        Effect(rune.Target, rune.TargetMap);
                    else
                        Caster.SendLocalizedMessage(501805); // That rune is not yet marked.

                    return;
                }
                case Runebook runebook:
                {
                    var entry = runebook.Default ?? runebook.Entries.FirstOrDefault();

                    if (entry is null)
                        Caster.SendLocalizedMessage(502423); // This place in the book is empty.
                    else
                        Effect(entry.Location, entry.Map);

                    return;
                }
                case Key key when key.KeyValue != 0 && key.Link is BaseBoat boat:
                {
                    if (!boat.Deleted && boat.CheckKey(key.KeyValue))
                        Effect(boat.GetMarkedLocation(), boat.Map, false);
                    return;
                }
            }

            Caster.SendLocalizedMessage(502357); // I can not recall from that object.
        }

        private void Effect(Point3D loc, Map map, bool checkMulti = true)
        {
            if (map == null || Caster.Map != map)
            {
                Caster.SendLocalizedMessage(1005569); // You can not recall to another facet.
                return;
            }

            if (!SpellHelper.CheckTravel(Caster, TravelCheckType.RecallFrom))
                return;

            if (!SpellHelper.CheckTravel(Caster, map, loc, TravelCheckType.RecallTo))
                return;
            
            if (!map.CanSpawnMobile(loc.X, loc.Y, loc.Z))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
                return;
            }

            if (checkMulti && loc.GetMulti(map)?.IsMultiFriend(Caster) == false)
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
                return;
            }

            if (m_Book != null && m_Book.CurCharges <= 0)
            {
                Caster.SendLocalizedMessage(502412); // There are no charges left on that item.
                return;
            }

            BaseCreature.TeleportPets(Caster, loc, map, true);

            if (m_Book != null)
                --m_Book.CurCharges;

            Caster.PlaySound(0x1FC);
            Caster.MoveToWorld(loc, map);
            Caster.PlaySound(0x1FC);
        }
    }
}