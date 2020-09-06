using System;
using System.Collections.Generic;
using Scripts.Engines.Magic;
using Server;
using Server.Commands;
using Server.Items;

namespace ZuluContent.Zulu.Engines.Magic
{
    public partial interface IMagicEquipItem
    {
        public IEntity Parent { get; set; }

        public MagicalProperties MagicProps { get; }

        public int DexBonus
        {
            get => MagicProps.TryGetMod(StatType.Dex, out MagicStatMod mod) ? mod.Offset : 0;
            set => MagicProps.AddMod(new MagicStatMod(StatType.Dex, value));
        }
        
        public int StrBonus
        {
            get => MagicProps.TryGetMod(StatType.Str, out MagicStatMod mod) ? mod.Offset : 0;
            set => MagicProps.AddMod(new MagicStatMod(StatType.Str, value));
        }
        
        public int IntBonus
        {
            get => MagicProps.TryGetMod(StatType.Int, out MagicStatMod mod) ? mod.Offset : 0;
            set => MagicProps.AddMod(new MagicStatMod(StatType.Int, value));
        }
    }
}