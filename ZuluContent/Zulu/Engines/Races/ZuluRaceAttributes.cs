using System.Linq;
using Server;

namespace Scripts.Zulu.Engines.Races
{
    public record ZuluRaceAttributes
    {
        public int Hue { get; init; }
        public Point3D Location { get; init; }
        public Map Map { get; init; }
    }
}