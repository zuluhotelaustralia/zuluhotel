using System.Threading.Tasks;
using Server.Items;
using Server.Misc;
using Server.Regions;
using Server.Spells.Fifth;
using Server.Spells.Fourth;
using Server.Spells.Sixth;
using Server.Targeting;

namespace Server.Spells.Third
{
    public class TeleportSpell : MagerySpell, ITargetableAsyncSpell<IPoint3D>
    {
        public TeleportSpell(Mobile caster, Item spellItem) : base(caster, spellItem) { }
        
        public async Task OnTargetAsync(ITargetResponse<IPoint3D> response)
        {
            if (!response.HasValue)
                return;

            var point = response.Target;
            
            var orig = point;
            var map = Caster.Map;

            SpellHelper.GetSurfaceTop(ref point);

            var from = Caster.Location;
            var to = new Point3D(point);

            if (!SpellHelper.CheckTravel(Caster, TravelCheckType.TeleportFrom))
            {
                Caster.SendLocalizedMessage(502361); // You cannot teleport into that area from here.
                return;
            }
            
            if (!SpellHelper.CheckTravel(Caster, map, to, TravelCheckType.TeleportTo))
            {
                Caster.SendLocalizedMessage(502360); // You cannot teleport into that area.
                return;
            }
            
            if (map == null || !map.CanSpawnMobile(to))
            {
                Caster.SendLocalizedMessage(501942); // That location is blocked.
                return;
            }
            
            if (SpellHelper.CheckMulti(to, map) || Region.Find(to, map).GetRegion(typeof(HouseRegion)) != null)
            {
                Caster.SendLocalizedMessage(502831); // Cannot teleport to that spot.
                return;
            }
            
            SpellHelper.Turn(Caster, orig);

            Caster.Location = to;
            Caster.ProcessDelta();
            
            if (Caster.Player)
            {
                Effects.SendLocationParticles(EffectItem.Create(from, Caster.Map, EffectItem.DefaultDuration), 0x3728,
                    10, 10, 2023);
                Effects.SendLocationParticles(EffectItem.Create(to, Caster.Map, EffectItem.DefaultDuration), 0x3728, 10,
                    10, 5023);
            }
            else
            {
                Caster.FixedParticles(0x376A, 9, 32, 0x13AF, EffectLayer.Waist);
            }

            Caster.PlaySound(0x1FE);

            IPooledEnumerable eable = Caster.GetItemsInRange(0);

            foreach (Item item in eable)
                if (item is ParalyzeFieldSpell.InternalItem || item is PoisonFieldSpell.PoisonFieldItem ||
                    item is FireFieldSpell.FireFieldItem)
                    item.OnMoveOver(Caster);

            eable.Free();
        }
    }
}