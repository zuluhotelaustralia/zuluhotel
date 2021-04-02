using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Multis;
using Server.Network;
using Server.Targeting;
using ZuluContent.Multis;

namespace Server.Spells.Seventh
{
    public class GateTravelSpell : MagerySpell, ITargetableAsyncSpell<Item>
    {
        private readonly RunebookEntry m_Entry;

        [SuppressMessage("ReSharper", "RedundantOverload.Global")]
        public GateTravelSpell(Mobile caster, Item spellItem) : this(caster, spellItem, null)
        {
        }
        
        public GateTravelSpell(Mobile caster, Item spellItem, RunebookEntry entry = null) : base(caster, spellItem)
        {
            m_Entry = entry;
        }
        
        async Task IAsyncSpell.CastAsync()
        {
            if (m_Entry == null)
                await (this as ITargetableAsyncSpell<Item>).SendTargetAsync();
            else
                Effect(m_Entry.Location, m_Entry.Map, true);
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
                        Effect(rune.Target, rune.TargetMap, true);
                    else
                        Caster.SendFailureMessage(501805); // That rune is not yet marked.

                    return;
                }
                case Runebook runebook:
                {
                    var entry = runebook.Default ?? runebook.Entries.FirstOrDefault();

                    if (entry is null)
                        Caster.SendFailureMessage(502423); // This place in the book is empty.
                    else
                        Effect(entry.Location, entry.Map, true);

                    return;
                }
                case Key key when key.KeyValue != 0 && key.Link is BaseBoat boat:
                {
                    if (!boat.Deleted && boat.CheckKey(key.KeyValue))
                        Effect(boat.GetMarkedLocation(), boat.Map, false);
                    return;
                }
            }

            Caster.SendFailureMessage(501030); // I can not gate travel from that object.
        }
        
        private void Effect(Point3D loc, Map map, bool checkMulti)
        {
            if (map == null || Caster.Map != map)
            {
                Caster.SendFailureMessage(1005569); // You can not recall to another facet.
                return;
            }

            if (!SpellHelper.CheckTravel(Caster, TravelCheckType.GateFrom))
                return;

            if (!SpellHelper.CheckTravel(Caster, map, loc, TravelCheckType.GateTo))
                return;
            
            if (!map.CanSpawnMobile(loc.X, loc.Y, loc.Z))
            {
                Caster.SendFailureMessage(501942); // That location is blocked.
                return;
            }

            if (checkMulti && loc.GetMulti(map)?.IsMultiFriend(Caster) == false)
            {
                Caster.SendFailureMessage(501942); // That location is blocked.
                return;
            }
            
            Caster.SendSuccessMessage(501024); // You open a magical gate to another location

            Effects.PlaySound(Caster.Location, Caster.Map, 0x20E);

            var firstGate = new Moongate(loc, map) {Dispellable = true};
            firstGate.MoveToWorld(Caster.Location, Caster.Map);

            Effects.PlaySound(loc, map, 0x20E);

            var secondGate = new Moongate(Caster.Location, Caster.Map) {Dispellable = true};
            secondGate.MoveToWorld(loc, map);

            CloseGatesAfterDelay(TimeSpan.FromSeconds(30.0), firstGate, secondGate);
        }

        private static async void CloseGatesAfterDelay(TimeSpan delay, params Moongate[] gates)
        {
            await Timer.Pause(delay);

            foreach (var gate in gates)
            {
                if(gate?.Deleted == false)
                    gate.Delete();
            }
        }
    }
}