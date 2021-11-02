using System;
using System.Collections.Generic;
using Server.Gumps;
using Server.Items;
using Server.Network;
using Server.Targeting;
using ZuluContent.Zulu.Engines.Magic.Enums;
using ZuluContent.Zulu.Items;

namespace Server.Mobiles
{
    [Serializable(0, false)]
    public partial class WaterElementShrineLord : BaseShrineLord
    {
        public override bool CanTeach => false;

        [Constructible]
        public WaterElementShrineLord()
        {
            Title = "the Water Element Shrine Lord";

            Hue = 1181;

            ShrineAcceptType = typeof(WaterElementalPentagram);
        }

        public override void InitBody()
        {
            base.InitBody();
            Name += " the Water Element Shrine Lord";
        }

        public override bool ClickTitle => false; // Do not display title in OnSingleClick
    }
}