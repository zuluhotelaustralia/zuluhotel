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
    public partial class AirElementShrineLord : BaseShrineLord
    {
        public override bool CanTeach => false;

        public override Type[] ShrineAcceptTypes => new[] { typeof(AirElementalPentagram) };

        [Constructible]
        public AirElementShrineLord()
        {
            Title = "the Air Element Shrine Lord";

            Body = 0x0d;

            Hue = 1161;
        }

        public override void InitBody()
        {
            base.InitBody();
            Name += " the Air Element Shrine Lord";
        }

        public override bool ClickTitle => false; // Do not display title in OnSingleClick
    }
}