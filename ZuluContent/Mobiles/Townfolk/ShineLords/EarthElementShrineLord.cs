using System;
using System.Collections.Generic;
using ModernUO.Serialization;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Mobiles
{
    [SerializationGenerator(0, false)]
    public partial class EarthElementShrineLord : BaseShrineLord
    {
        public override bool CanTeach => false;

        public override Type[] ShrineAcceptTypes => new[] { typeof(EarthElementalPentagram) };

        [Constructible]
        public EarthElementShrineLord()
        {
            Title = "the Earth Element Shrine Lord";

            Body = 0xE;

            Hue = 1134;
        }

        public override void InitBody()
        {
            base.InitBody();
            Name += " the Earth Element Shrine Lord";
        }

        public override bool ClickTitle => false; // Do not display title in OnSingleClick
    }
}