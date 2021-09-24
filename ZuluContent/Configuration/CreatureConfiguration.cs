using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Scripts.Configuration
{
    public class CreatureConfiguration : BaseSingleton<CreatureConfiguration>
    {
        public Dictionary<string, CreatureProperties> Entries =>
            CueConfiguration.Instance.RootConfig.Creatures;

        protected CreatureConfiguration()
        {
        }
    }
}