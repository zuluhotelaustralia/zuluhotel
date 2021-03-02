using System;
using System.Collections.Generic;
using System.Linq;
using Server;
using Server.Commands;
using Server.Items;

namespace ZuluContent.Zulu.Engines.Magic.Enums
{
    public partial interface IMagicItem : IEnchanted
    {
        public IEntity Parent { get; set; }

        public void OnSingleClick(Mobile m);

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Identified
        {
            get;
            set;
        }
    }
}