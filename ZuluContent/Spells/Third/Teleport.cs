using System.Threading.Tasks;
using Scripts.Zulu.Utilities;
using Server.Items;
using Server.Misc;
using Server.Regions;
using Server.Spells.Fifth;
using Server.Spells.Fourth;
using Server.Spells.Sixth;
using Server.Targeting;
using ZuluContent.Multis;

namespace Server.Spells.Third
{
    public class TeleportSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public TeleportSpell(Mobile caster, Item spellItem = null) : base(caster, spellItem)
        {
        }

        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = SpellHelper.GetSurfaceTop(response.Target);

            var orig = point;
            var map = Caster.Map;

            var from = Caster.Location;

            if (!SpellHelper.CheckTravel(Caster, TravelCheckType.TeleportFrom))
            {
                Caster.SendFailureMessage(502361); // You cannot teleport into that area from here.
                return;
            }

            if (!SpellHelper.CheckTravel(Caster, map, point, TravelCheckType.TeleportTo))
            {
                Caster.SendFailureMessage(502360); // You cannot teleport into that area.
                return;
            }

            if (map == null || !map.CanSpawnMobile(point))
            {
                Caster.SendFailureMessage(501942); // That location is blocked.
                return;
            }

            if (point.GetMulti(map)?.IsMultiFriend(Caster) == false)
            {
                Caster.SendFailureMessage(502831); // Cannot teleport to that spot.
                return;
            }

            SpellHelper.Turn(Caster, orig);

            Caster.Location = point;
            Caster.ProcessDelta();

            if (Caster.Player)
            {
                Effects.SendLocationParticles(EffectItem.Create(from, Caster.Map, EffectItem.DefaultDuration), 0x3728,
                    10, 10, 2023);
                Effects.SendLocationParticles(EffectItem.Create(point, Caster.Map, EffectItem.DefaultDuration), 0x3728, 10,
                    10, 5023);
            }
            else
            {
                Caster.FixedParticles(0x376A, 9, 32, 0x13AF, EffectLayer.Waist);
            }

            Caster.PlaySound(0x1FE);

            IPooledEnumerable eable = Caster.GetItemsInRange(0);

            foreach (Item item in eable)
            {
                if (item is FieldItem)
                    item.OnMoveOver(Caster);
            }

            eable.Free();
        }
    }
}