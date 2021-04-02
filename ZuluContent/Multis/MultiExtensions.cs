using Server;
using Server.Items;
using Server.Multis;

namespace ZuluContent.Multis
{
    public static class MultiExtensions
    {
        public static BaseMulti GetMulti(this Point3D p, Map map, bool houses = true, int housingRange = 0)
        {
            if (map == null || map == Map.Internal)
                return null;

            var sector = map.GetSector(p.X, p.Y);

            foreach (var multi in sector.Multis)
            {
                if (multi is BaseHouse bh)
                {
                    if (houses && bh.IsInside(p, 16) || housingRange > 0 && bh.InRange(p, housingRange))
                        return multi;
                }
                else if (multi.Contains(p))
                {
                    return multi;
                }
            }

            return null;
        }

        public static BaseMulti GetMulti(this Mobile mobile, bool houses = true, int housingRange = 0) =>
            GetMulti(mobile.Location, mobile.Map, houses, housingRange);

        public static bool IsMultiOwner(this BaseMulti multi, Mobile mobile) => multi switch
        {
            BaseHouse house => house.IsCoOwner(mobile),
            BaseBoat boat => boat.Owner == mobile,
            _ => false
        };

        public static bool IsMultiFriend(this BaseMulti multi, Mobile mobile) => multi switch
        {
            BaseHouse house => house.IsFriend(mobile) || house.IsMultiOwner(mobile),
            BaseBoat boat => boat.IsMultiOwner(mobile),
            _ => false
        };
    }
}