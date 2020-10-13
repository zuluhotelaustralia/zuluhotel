using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Commands;
using Server.Items;

namespace ZuluContent.Zulu.Engines.Magic.Enums
{
    public partial interface IMagicItem
    {
        public IEntity Parent { get; set; }
        
        public EnchantmentDictionary Enchantments { get; }
        
        public void OnSingleClick(Mobile m);
        
        
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Identified
        {
            get;
            set;
        }
    }
}