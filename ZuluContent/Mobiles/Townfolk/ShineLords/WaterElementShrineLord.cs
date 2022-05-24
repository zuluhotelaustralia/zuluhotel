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
    public partial class WaterElementShrineLord : BaseShrineLord
    {
        public override bool CanTeach => false;

        public override Type[] ShrineAcceptTypes => new[] { typeof(WaterElementalPentagram), typeof(Shell) };

        [Constructible]
        public WaterElementShrineLord()
        {
            Title = "the Water Element Shrine Lord";

            Body = 0x10;

            Hue = 1181;
        }

        public override void InitBody()
        {
            base.InitBody();
            Name += " the Water Element Shrine Lord";
        }

        public override bool ClickTitle => false; // Do not display title in OnSingleClick
    }
}