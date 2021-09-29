using System;
using System.Collections.Generic;
using Server;
using Server.Scripts.Engines.Loot;

namespace Scripts.Configuration
{
    public class LootConfiguration : BaseSingleton<LootConfiguration>
    {
        public IReadOnlyDictionary<string, LootTable> Tables => CueConfiguration.Instance.RootConfig.Loot.Tables;

        protected LootConfiguration()
        {
        }
    }
}