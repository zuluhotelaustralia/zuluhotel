using System;
using System.Collections;
using System.Linq;
using Server;
using Server.Network;
using Server.Items;
using Server.Targeting;
using Server.Spells;
using Server.Mobiles;
using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server.Multis;
using ZuluContent.Multis;

namespace Scripts.Zulu.Spells.Earth
{
    public class EarthPortalSpell : EarthSpell, ITargetableAsyncSpell<Item>
    {
        public EarthPortalSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem) { }
        
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

            var firstGate = new BlackMoongate(loc, map) {Dispellable = true};
            firstGate.MoveToWorld(Caster.Location, Caster.Map);

            Effects.PlaySound(loc, map, 0x20E);

            var secondGate = new BlackMoongate(Caster.Location, Caster.Map) {Dispellable = true};
            secondGate.MoveToWorld(loc, map);

            CloseGatesAfterDelay(TimeSpan.FromSeconds(Caster.Skills[SkillName.Magery].Value / 2), firstGate, secondGate);
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