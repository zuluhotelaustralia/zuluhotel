using Server;

namespace Scripts.Zulu.Engines.Classes
{
    public interface IZuluClassed
    {
        public ZuluClass ZuluClass { get; }
        
        public Skills Skills { get; }
    }
}