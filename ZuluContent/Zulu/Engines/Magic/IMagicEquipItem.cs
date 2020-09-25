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
    }
}