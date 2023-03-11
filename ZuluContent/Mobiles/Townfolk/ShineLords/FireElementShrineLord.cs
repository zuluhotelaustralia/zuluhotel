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
    public partial class FireElementShrineLord : BaseShrineLord
    {
        public override bool CanTeach => false;

        public override Type[] ShrineAcceptTypes => new[] { typeof(FireElementalPentagram) };

        [Constructible]
        public FireElementShrineLord()
        {
            Title = "the Fire Element Shrine Lord";

            Body = 0x0f;

            Hue = 1172;
        }

        public override void InitBody()
        {
            base.InitBody();
            Name += " the Fire Element Shrine Lord";
        }

        public override bool ClickTitle => false; // Do not display title in OnSingleClick
    }
}