using Server;

namespace Scripts.Zulu.Engines.Races
{
    public interface IZuluRace
    {
        public ZuluRace ZuluRace { get; }
        
        public ZuluRaceType ZuluRaceType { get; set; }
    }
}